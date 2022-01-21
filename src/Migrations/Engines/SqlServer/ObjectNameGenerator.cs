using System.Collections.Generic;
using System.Linq;
using Migrations.Extensions;
using Migrations.Models;

namespace Migrations.Engines.SqlServer;

/// <summary>
/// Can generate full-qualified names for database objects.
/// </summary>
public static class ObjectNameGenerator
{
    public static string ToObjectName(this SchemaDefinition definition)
    {
        var components = new List<string>();
        
        if (!string.IsNullOrEmpty(definition.Database))
        {
            components.Add(definition.Database);
        }

        if (!string.IsNullOrEmpty(definition.Schema))
        {
            components.Add(definition.Schema);
        }

        components.Add(definition.Name);

        return string.Join('.', components.Select(SqlArg.Bracketize));
    }
}