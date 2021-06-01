using System.Data;

namespace Migrations.Builder
{
    public class ColumnDefinition
    {
        public virtual string Name { get; set; }
        public virtual int Size { get; set; }
        public virtual int Precision { get; set; }
        public virtual SqlDbType SqlType { get; set; }
        public virtual string DefaultValue { get; set; }
        public virtual bool IsForeignKey { get; set; }
        public virtual bool IsIdentity { get; set; }
        public virtual bool IsIndexed { get; set; }
        public virtual bool IsPrimaryKey { get; set; }
        public virtual string PrimaryKeyName { get; set; }
        public virtual bool? IsNullable { get; set; }
        public virtual bool IsUnique { get; set; }
        public virtual string TableName { get; set; }
        public virtual string ColumnDescription { get; set; }
        public virtual string CollationName { get; set; }
        public SchemaOperation Operation { get; set; }
        public SchemaCondition Condition { get; set; }
    }
}