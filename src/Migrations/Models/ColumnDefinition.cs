using System.Data;

namespace Migrations.Models
{
    public class ColumnDefinition : SchemaDefinition
    {
        public ColumnDefinition()
        {
        }

        public ColumnDefinition(string name, SqlDbType sqlType) : this(name)
        {
            SqlType = sqlType;
        }
        public ColumnDefinition(string name, SqlDbType sqlType, int precision) : this(name, sqlType)
        {
            Precision = precision;
        }

        public ColumnDefinition(string name)
        {
            Name = name;
        }

        public int Size { get; set; }

        public int Precision { get; set; }
        
        public SqlDbType SqlType { get; set; }

        public string DefaultValue { get; set; }
        public bool IsForeignKey { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsIndexed { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string PrimaryKeyName { get; set; }
        public bool? IsNullable { get; set; }
        public bool IsUnique { get; set; }
        public string TableName { get; set; }
        public string ColumnDescription { get; set; }
        public string CollationName { get; set; }
    }
}