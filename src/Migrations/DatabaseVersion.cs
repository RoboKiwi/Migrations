using System;
using System.Threading.Tasks;
using Migrations.Extensions;

namespace Migrations
{
    /// <summary>
    ///     An abstract base implementation of <see cref="IDatabaseVersion" /> that
    ///     normalizes the version to 4 digits.
    /// </summary>
    public abstract class DatabaseVersion : IDatabaseVersion
    {
        public async Task<Version> GetVersionAsync()
        {
            var currentVersion = await GetVersionRawAsync();
            return currentVersion.Normalize();
        }

        public abstract Task SetVersionAsync(Version version);

        protected abstract Task<Version> GetVersionRawAsync();
    }
}