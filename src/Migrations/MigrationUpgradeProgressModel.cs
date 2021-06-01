using System.Diagnostics;

namespace Migrations
{
    public class MigrationUpgradeProgressModel
    {
        public string Action { get; set; }
        public int Progress { get; set; }
        public TraceLevel TraceLevel { get; set; }
    }
}