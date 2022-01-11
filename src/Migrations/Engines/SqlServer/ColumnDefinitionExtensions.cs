using System;
using System.Data;
using Migrations.Models;

namespace Migrations.Engines.SqlServer;

public static class ColumnDefinitionExtensions
{
    public static string ToSqlType(this ColumnDefinition def)
    {
        switch (def.SqlType)
        {
            case SqlDbType.BigInt: return "bigint";

            case SqlDbType.Binary:
                break;
            case SqlDbType.Bit:
                break;
            case SqlDbType.Char:
                return def.Precision switch
                {
                    >= 1 and <= 8000 => "char(" + def.Precision + ")",
                    > 8000 => "char(8000)",
                    < 1 => "char(1)"
                };

            case SqlDbType.DateTime:
                break;
            case SqlDbType.Decimal:
                break;
            case SqlDbType.Float:
                break;
            case SqlDbType.Image:
                break;
            case SqlDbType.Int:
                switch (def.Precision)
                {
                    case 0:
                    case 4:
                        return "int";
                    case 8:
                        return "bigint";
                    case 2:
                        return "smallint";
                    case 1:
                        return "tinyint";
                }

                break;

            case SqlDbType.Money:
                break;
            case SqlDbType.NChar:
                return def.Precision switch
                {
                    >= 1 and <= 4000 => "nchar(" + def.Precision + ")",
                    > 4000 => "nchar(4000)",
                    < 1 => "nchar(1)"
                };
                
            case SqlDbType.NText:
                break;
            case SqlDbType.NVarChar:
                return def.Precision is > 0 and <= 4000 ? "nvarchar(" + def.Precision + ")" : "nvarchar(max)";

            case SqlDbType.Real:
                break;
            case SqlDbType.UniqueIdentifier:
                break;
            case SqlDbType.SmallDateTime:
                break;
            case SqlDbType.SmallInt: return "smallint";

            case SqlDbType.SmallMoney:
                break;
            case SqlDbType.Text:
                break;
            case SqlDbType.Timestamp:
                break;
            case SqlDbType.TinyInt: return "tinyint";
            case SqlDbType.VarBinary:
                break;
            case SqlDbType.VarChar:
                return def.Precision is > 0 and <= 8000 ? "varchar(" + def.Precision + ")" : "varchar(max)";

            case SqlDbType.Variant:
                break;
            case SqlDbType.Xml:
                break;
            case SqlDbType.Udt:
                break;
            case SqlDbType.Structured:
                break;
            case SqlDbType.Date:
                break;
            case SqlDbType.Time:
                break;
            case SqlDbType.DateTime2:
                break;
            case SqlDbType.DateTimeOffset:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        throw new NotSupportedException();
    }
}