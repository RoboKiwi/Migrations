using System;
using System.Threading.Tasks;

namespace Migrations
{
    /// <summary>
    ///     Allows getting and setting the current version of the database.
    /// </summary>
    /// <remarks>
    ///     Implementations of this contract are responsible for executing the code or SQL for
    ///     getting and setting the version of the underlying application database.
    /// </remarks>
    public interface IDatabaseVersion
    {
        /// <summary>
        ///     Gets the current version of the database.
        /// </summary>
        /// <returns></returns>
        Task<Version> GetVersionAsync();

        /// <summary>
        ///     Updates the current version of the database.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        Task SetVersionAsync(Version version);
    }
}