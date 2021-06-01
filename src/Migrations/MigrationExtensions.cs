using System.Reflection;

namespace Migrations
{
    public static class MigrationExtensions
    {
        /// <summary>
        ///     Gets the <see cref="MigrationAttribute" /> for the migration definition.
        /// </summary>
        /// <param name="migration"></param>
        /// <returns></returns>
        public static MigrationAttribute GetAttribute(this IMigration migration)
        {
            return migration.GetType().GetCustomAttribute<MigrationAttribute>();
        }
    }
}