using System;

namespace Migrations
{
    public class MigrationAttribute : Attribute
    {
        public MigrationAttribute(string description)
        {
            Description = description;
        }

        public MigrationAttribute(string description, string version)
        {
            Description = description;
            Version = new Version(version);
        }

        public string Description { get; set; }

        public Version Version { get; set; }
    }
}