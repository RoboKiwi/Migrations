using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Migrations
{
    public abstract class AggregateMigration : IMigration, IAggregateMigration
    {
        public abstract IEnumerable<Type> GetChildMigrations();

        public virtual Task Up()
        {
            return Task.CompletedTask;
        }

        public virtual Task Down()
        {
            return Task.CompletedTask;
        }
    }
}