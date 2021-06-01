using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Migrations.Extensions;

namespace Migrations.Builder
{
    public class MigrationBuilder
    {
        readonly DbConnection connection;

        public MigrationBuilder(DbConnection connection)
        {
            this.connection = connection;
        }

        string Bracketize(string name)
        {
            return SqlArg.Bracketize(name);
        }

        public ColumnBuilder Column(string table, string name)
        {
            return new(table, name);
        }

        public async Task Execute(ColumnDefinition column, CancellationToken token = default)
        {
            var columnExists = await connection.ColumnExistsAsync(column.TableName, column.Name, token);
            if (column.Condition == SchemaCondition.IfExists && columnExists) return;
            if (column.Condition == SchemaCondition.IfExists && !columnExists) return;

            var sb = new StringBuilder();

            // Specify the table operation
            sb.Append("ALTER TABLE " + SqlArg.Bracketize(column.TableName));

            // Column operation
            sb.Append(ColumnOperationToSql(column.Operation));

            // Column name
            sb.Append(" COLUMN ").Append(Bracketize(column.Name));

            // Column Type
            sb.Append(" ").Append(ColumnTypeToSql(column));

            // Nullability
            if (column.IsNullable == false) sb.Append(" NOT");
            sb.Append(" NULLABLE");

            // Default value
            if (column.DefaultValue != null) sb.Append(" DEFAULT (").Append(column.DefaultValue).Append(")");

            var reader = new StringReader(sb.ToString());

            await connection.ExecuteBatchNonQueryAsync(token, reader);
        }

        string ColumnTypeToSql(ColumnDefinition column)
        {
            switch (column.SqlType)
            {
                //                case SqlDbType.BigInt:
                //                    break;
                //                case SqlDbType.Binary:
                //                    break;
                //                case SqlDbType.Bit:
                //                    break;
                //                case SqlDbType.Char:
                //                    break;
                //                case SqlDbType.Date:
                //                    break;
                //                case SqlDbType.DateTime:
                //                    break;
                //                case SqlDbType.DateTime2:
                //                    break;
                //                case SqlDbType.DateTimeOffset:
                //                    break;
                //                case SqlDbType.Decimal:
                //                    break;
                //                case SqlDbType.Float:
                //                    break;
                //                case SqlDbType.Image:
                //                    break;
                case SqlDbType.Int:
                    return Bracketize("int");

                //                case SqlDbType.Money:
                //                    break;
                //                case SqlDbType.NChar:
                //                    break;
                //                case SqlDbType.NText:
                //                    break;
                //                case SqlDbType.NVarChar:
                //                    break;
                //                case SqlDbType.Real:
                //                    break;
                //                case SqlDbType.SmallDateTime:
                //                    break;
                //                case SqlDbType.SmallInt:
                //                    break;
                //                case SqlDbType.SmallMoney:
                //                    break;
                //                case SqlDbType.Structured:
                //                    break;
                //                case SqlDbType.Text:
                //                    break;
                //                case SqlDbType.Time:
                //                    break;
                //                case SqlDbType.Timestamp:
                //                    break;
                //                case SqlDbType.TinyInt:
                //                    break;
                //                case SqlDbType.Udt:
                //                    break;
                //                case SqlDbType.UniqueIdentifier:
                //                    break;
                //                case SqlDbType.VarBinary:
                //                    break;
                //                case SqlDbType.VarChar:
                //                    break;
                //                case SqlDbType.Variant:
                //                    break;
                //                case SqlDbType.Xml:
                //                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        string ColumnOperationToSql(SchemaOperation operation)
        {
            switch (operation)
            {
                case SchemaOperation.Create:
                    return "ADD";
                case SchemaOperation.Alter:
                    return "ALTER";
                case SchemaOperation.Drop:
                    return "DROP";
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
        }
    }
}