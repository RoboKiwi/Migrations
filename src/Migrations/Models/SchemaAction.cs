using System;

namespace Migrations.Builder
{
    [Flags]
    public enum SchemaAction
    {
        None = 0,
        Create = 1,
        Alter = 2,
        Drop = 4
    }
}