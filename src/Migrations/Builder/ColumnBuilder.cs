using System.Data;

namespace Migrations.Builder
{
    public class ColumnBuilder
    {
        SchemaCondition condition;
        string defaultValue;
        readonly string name;
        bool nullable;
        SchemaOperation operation;
        readonly string table;
        SqlDbType type;

        public ColumnBuilder(string name, string table)
        {
            this.name = name;
            this.table = table;
        }

        public ColumnBuilder Type(SqlDbType type)
        {
            this.type = type;
            return this;
        }

        public ColumnBuilder NotNullable()
        {
            return Nullable(false);
        }

        public ColumnBuilder Nullable()
        {
            return Nullable(true);
        }

        public ColumnBuilder Nullable(bool nullable)
        {
            this.nullable = nullable;
            return this;
        }

        public ColumnBuilder Create()
        {
            return Operation(SchemaOperation.Create);
        }

        public ColumnBuilder CreateIfNotExists()
        {
            return Operation(SchemaOperation.Create).Condition(SchemaCondition.IfNotExists);
        }

        public ColumnBuilder Condition(SchemaCondition condition)
        {
            this.condition = condition;
            return this;
        }

        public ColumnBuilder Operation(SchemaOperation operation)
        {
            this.operation = operation;
            return this;
        }

        public ColumnBuilder DefaultValue(string value)
        {
            defaultValue = value;
            return this;
        }

        public ColumnBuilder DefaultValue(object value)
        {
            defaultValue = value?.ToString();
            return this;
        }

        public ColumnDefinition Build()
        {
            return new()
            {
                TableName = table,
                Name = name,
                IsNullable = nullable,
                SqlType = type,
                Operation = operation,
                DefaultValue = defaultValue,
                Condition = condition
            };
        }
    }
}