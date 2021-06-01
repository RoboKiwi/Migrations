using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Migrations.Extensions;

namespace Migrations
{
    /// <summary>
    ///     Metadata about the migration including id and version.
    /// </summary>
    public class MigrationMetadata
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public Type Type { get; set; }

        public Version Version { get; set; }

        public MigrationMetadata ParentMigration { get; set; }

        /// <summary>
        ///     Acquires the migration metadata from the migration's type definition.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static MigrationMetadata GetFromType(Type type)
        {
            var attribute = type.GetCustomAttribute<MigrationAttribute>();
            var guidAttribute = type.GetCustomAttribute<GuidAttribute>();

            if (attribute == null) throw new MigrationException($"Migration is missing a Migration attribute: {type}");

            return new MigrationMetadata
            {
                Version = attribute.Version.Normalize(),
                Type = type,
                Description = attribute.Description,
                Id = guidAttribute?.Value
            };
        }
    }
}