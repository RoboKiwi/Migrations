using System;
using System.Collections.Generic;

namespace Migrations
{
    public interface IMigrationRegistry
    {
        IEnumerable<Type> GetMigrations();

        Version GetVersion();
    }
}