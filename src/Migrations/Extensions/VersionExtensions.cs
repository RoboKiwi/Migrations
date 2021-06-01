using System;

namespace Migrations.Extensions
{
    public static class VersionExtensions
    {
        /// <summary>
        ///     Normalizes a version to the full 4 digits; e.g. from 4.1.0 to 4.1.0.0.
        /// </summary>
        /// <remarks>
        ///     Normalize the version to 4 digits. Any missing digits come back as -1 (e.g. 7.3 is internally
        ///     represented as 7.3.-1.-1. This makes it impossible to compare versions properly until we
        ///     normalize them all to 4 required numbers.
        /// </remarks>
        /// <param name="version"></param>
        /// <returns></returns>
        public static Version Normalize(this Version version)
        {
            if (version == null) return null;

            return new Version(
                version.Major < 0 ? 0 : version.Major,
                version.Minor < 0 ? 0 : version.Minor,
                version.Build < 0 ? 0 : version.Build,
                version.Revision < 0 ? 0 : version.Revision);
        }
    }
}