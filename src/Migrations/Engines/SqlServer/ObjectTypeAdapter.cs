using System;
using Migrations.Models;

namespace Migrations.Engines.SqlServer;

public static class ObjectTypeAdapter
{
    public static string GetObjectType(SchemaDefinition definition)
    {
        // User-defined Table
        if (definition is TableDefinition) return "U";

        throw new ArgumentOutOfRangeException(nameof(definition));
    }
}