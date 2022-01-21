using System.Collections.Generic;

namespace Migrations.Models
{
    public class TableDefinition : SchemaDefinition
    {
        public TableDefinition()
        {
        }

        public TableDefinition(string name, string schema = null)
        {
            Name = name;
            Schema = schema;
        }
        
        public IList<ColumnDefinition> Columns { get; set; } = new List<ColumnDefinition>();
    }
}