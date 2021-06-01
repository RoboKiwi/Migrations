using System;
using System.Collections.Generic;

namespace Migrations
{
    public interface IAggregateMigration
    {
        IEnumerable<Type> GetChildMigrations();
    }
}