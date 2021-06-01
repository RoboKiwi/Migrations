using System;
using System.Diagnostics;

namespace Migrations
{
    public class MigrationProgress : IProgress<MigrationUpgradeProgressModel>
    {
        readonly Action<string, TraceLevel, int> messageHandler;

        public MigrationProgress() : this(null)
        {
        }

        public MigrationProgress(Action<string, TraceLevel, int> messageHandler)
        {
            this.messageHandler = messageHandler;
        }

        public void Report(MigrationUpgradeProgressModel value)
        {
            messageHandler?.Invoke(value.Action, value.TraceLevel, value.Progress);
        }
    }
}