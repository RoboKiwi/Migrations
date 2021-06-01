using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Migrations.Extensions;

namespace Migrations
{
    public abstract class EmbeddedSqlMigration : IMigration
    {
        readonly DbConnection connection;
        readonly string embeddedSqlName;

        protected EmbeddedSqlMigration(DbConnection connection, string embeddedSqlName)
        {
            this.connection = connection;
            this.embeddedSqlName = GetType().Namespace + "." + embeddedSqlName;
        }

        public virtual async Task Up()
        {
            var assembly = GetType().Assembly;
            using (var stream = assembly.GetManifestResourceStream(embeddedSqlName))
            {
                if (stream == null)
                    throw new MigrationException(
                        $"Couldn't load resource named '{embeddedSqlName}'. Available resource names: {string.Join("\r\n", assembly.GetManifestResourceNames())}");

                using (var reader = new StreamReader(stream))
                {
                    await connection.ExecuteBatchNonQueryAsync(CancellationToken.None, reader);
                }
            }
        }

        public virtual Task Down()
        {
            return TaskHelper.Failed;
        }
    }
}