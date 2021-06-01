using System;
using System.Diagnostics;

namespace Migrations
{
    static class MigrationProgressExtensions
    {
        public static void Report(this IProgress<MigrationUpgradeProgressModel> @this, string message)
        {
            @this.Report(-1, message);
        }

        public static void Report(this IProgress<MigrationUpgradeProgressModel> @this, string message,
            params object[] args)
        {
            @this.Report(-1, message, args);
        }

        public static void Report(this IProgress<MigrationUpgradeProgressModel> @this, TraceLevel level, int progress,
            string message)
        {
            @this.Report(new MigrationUpgradeProgressModel {Action = message, Progress = progress, TraceLevel = level});
        }

        public static void Report(this IProgress<MigrationUpgradeProgressModel> @this, TraceLevel level, int progress,
            string message, params object[] args)
        {
            var line = string.Format(message, args);
            @this.Report(level, progress, line);
        }

        public static void Report(this IProgress<MigrationUpgradeProgressModel> @this, int progress, string message)
        {
            @this.Report(TraceLevel.Info, progress, message);
        }

        public static void Report(this IProgress<MigrationUpgradeProgressModel> @this, int progress, string message,
            params object[] args)
        {
            var line = string.Format(message, args);
            @this.Report(progress, line);
        }
    }
}