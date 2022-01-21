using System.Collections.Generic;
using Migrations.Builder;

namespace Migrations.Models;

public class DatabaseDefinition
{
    public virtual string Name { get; set; }

    public virtual IList<string> Schemas { get; set; } = new List<string>();

    public IList<TableDefinition> Tables { get; set; } = new List<TableDefinition>();

    public SchemaAction Action { get; set; }

    public SchemaCondition Condition { get; set; }
}