using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Migrations.Container;
using Migrations.Extensions;

namespace Migrations
{
    public abstract class Migrator : IDisposable
    {
        protected Migrator()
        {
            Options = new MigrationOptions();
            Result = new MigrationResult();
        }

        public MigrationResult Result { get; protected set; }

        public MigrationOptions Options { get; protected set; }

        public IContainer Container { get; protected set; }

        public IProgress<MigrationUpgradeProgressModel> Progress { get; protected set; }

        public CancellationToken CancellationToken { get; protected set; }

        public DbConnection Connection { get; protected set; }

        public IDatabaseVersion DatabaseVersion { get; protected set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual async Task<MigrationResult> ExecuteAsync()
        {
            await Initialize();

            Result = new MigrationResult();

            Progress.Report("Connecting to database...");
            await Connection.OpenAsync(CancellationToken);
            Progress.Report($"Connected to {Connection.DataSource}");
            Progress.Report($"Opened {Connection.Database}");

            // If the user didn't specify a database name to migrate, try to get it from the connection string.
            if (string.IsNullOrWhiteSpace(Options.Database))
            {
                if (!string.IsNullOrWhiteSpace(Connection.Database) &&
                    !"master".Equals(Connection.Database, StringComparison.OrdinalIgnoreCase))
                    Options.Database = Connection.Database;
                else
                    throw new MigrationException("No database name was specified to migrate");
            }

            Result.Database = Options.Database;

            // Check if the database already exists
            Result.DatabaseExists = await Connection.DatabaseExists(Options.Database, CancellationToken);

            // If we're forcing create, then drop the database to force creation
            if (Result.DatabaseExists)
            {
                Trace.TraceInformation($"Database '{Options.Database}' exists.");
                if (Options.Create && Options.Force)
                {
                    Connection.ChangeDatabase("master");
                    await Connection.ExecuteNonQueryAsync(CancellationToken,
                        $"ALTER DATABASE {SqlArg.Bracketize(Options.Database)} SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                    await Connection.ExecuteNonQueryAsync(CancellationToken,
                        $"DROP DATABASE {SqlArg.Bracketize(Options.Database)}");
                    Result.DatabaseExists = false;
                }
            }

            // Create the database if it doesn't exist
            if (!Result.DatabaseExists)
            {
                Trace.TraceInformation("Database doesn't exist.");
                if (!Options.Create) throw new MigrationException("Couldn't migrate database as it can't be found");
                Progress.Report($"Creating database '{Options.Database}'...");
                await Connection.ExecuteNonQueryAsync(CancellationToken,
                    $"CREATE DATABASE {SqlArg.Bracketize(Options.Database)} COLLATE SQL_Latin1_General_CP1_CI_AS");
                Connection.ChangeDatabase(Options.Database);
                Result.DatabaseCreated = true;
            }

            Progress.Report("Getting current database version");
            Result.StartingVersion = Result.CurrentVersion = await DatabaseVersion.GetVersionAsync();

            if (Options.From != null && Result.CurrentVersion < Options.From)
                throw new MigrationException(
                    $"You can't upgrade from version {Options.From} as you're only at version {Result.CurrentVersion}");
            if (Options.To != null && Result.CurrentVersion > Options.To)
                throw new MigrationException("Downgrading is not supported");

            foreach (var type in GetMigrations())
            {
                var metadata = MigrationMetadata.GetFromType(type);

                if (Options.To != null && metadata.Version > Options.To)
                {
                    // The migration is newer than the target version
                    Progress.Report("Skipping migration {0}: {1}", metadata.Version, metadata.Description);
                    continue;
                }

                if (metadata.Version < Result.CurrentVersion)
                {
                    // The database is newer than this migration.
                    Progress.Report("Skipping migration {0}: {1}", metadata.Version, metadata.Description);
                    continue;
                }

                if (metadata.Version == Result.CurrentVersion)
                {
                    Progress.Report("Database is already up to date at migration {0}: {1}", metadata.Version,
                        metadata.Description);
                    continue;
                }

                Progress.Report("Running migration {0}:{1}", metadata.Version, metadata.Description);

                await ExecuteMigrationAsync(metadata);
            }

            return Result;
        }

        protected abstract IEnumerable<Type> GetMigrations();

        protected virtual async Task ExecuteMigrationAsync(MigrationMetadata metadata)
        {
            var migration = (IMigration) Container.Resolve(metadata.Type);

            try
            {
                await migration.Up();

                if (migration is IAggregateMigration aggregate)
                    foreach (var childMigrationType in aggregate.GetChildMigrations())
                    {
                        var childMetadata = MigrationMetadata.GetFromType(childMigrationType);
                        childMetadata.ParentMigration = metadata;
                        await ExecuteMigrationAsync(childMetadata);
                    }

                if (metadata.Version != null)
                {
                    await DatabaseVersion.SetVersionAsync(metadata.Version);
                    Result.CurrentVersion = metadata.Version;
                }
            }
            catch (Exception ex)
            {
                Progress.Report("{0} failed: {1}", migration, ex);

                try
                {
                    Progress.Report("Rolling back {0}...", migration);
                    await migration.Down();
                    Progress.Report("Rollback succeeded.");
                }
                catch (Exception downException)
                {
                    Progress.Report("Couldn't roll back {0}: {1}", migration, downException);
                    throw;
                }

                throw;
            }
        }

        protected virtual Task Initialize()
        {
            Container.Register<DbConnection>(Connection);

            return TaskHelper.Completed;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            Container?.Dispose();
        }
    }
}