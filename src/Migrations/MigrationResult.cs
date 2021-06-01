using System;

namespace Migrations
{
    public class MigrationResult
    {
        public bool DatabaseCreated { get; set; }

        public bool DatabaseExists { get; set; }

        public Version CurrentVersion { get; set; }

        public Version StartingVersion { get; set; }

        public string Database { get; set; }
    }
}