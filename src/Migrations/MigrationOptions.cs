using System;
using System.Diagnostics;
using System.IO;

namespace Migrations
{
    public class MigrationOptions
    {
        public MigrationOptions()
        {
            Message = DefaultTraceAction;
        }

        /// <summary>
        ///     The name of the database to migrate
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        ///     The destination for database backups
        /// </summary>
        public string BackupDestination { get; set; }

        /// <summary>
        ///     Creates a backup of the database before migrating.
        /// </summary>
        public bool Backup { get; set; }

        /// <summary>
        ///     Creates a new database.
        /// </summary>
        public bool Create { get; set; }

        /// <summary>
        ///     The version to migrate to
        /// </summary>
        public Version To { get; set; }

        /// <summary>
        ///     The version to migrate from
        /// </summary>
        public Version From { get; set; }

        public string ConnectionString { get; set; }

        /// <summary>
        ///     Trace message callback
        /// </summary>
        public Action<string, TraceLevel, int> Message { get; set; }

        /// <summary>
        ///     Forces running migrations even if the database is up to date.
        /// </summary>
        public bool Force { get; set; }

        public TextWriter TraceTextWriter { get; set; }

        static void DefaultTraceAction(string s, TraceLevel level, int percent)
        {
            switch (level)
            {
                case TraceLevel.Off:
                    break;

                case TraceLevel.Error:
                    Trace.TraceError(s);
                    break;

                case TraceLevel.Warning:
                    Trace.TraceWarning(s);
                    break;

                case TraceLevel.Info:
                    Trace.TraceInformation(s);
                    break;

                case TraceLevel.Verbose:
                    Trace.WriteLine(s);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(level));
            }
        }
    }
}