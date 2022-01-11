using System.Data;
using Migrations.Engines.SqlServer;
using Migrations.Models;
using Xunit;

namespace Migrations.Tests.Engines.SqlServer;

public class ToSqlTypeTests
{
    [Theory]
    [InlineData("nchar(1)", SqlDbType.NChar, 0)]
    [InlineData("nchar(4000)", SqlDbType.NChar, 4001)]
    [InlineData("nchar(4000)", SqlDbType.NChar, 4000)]
    [InlineData("nchar(3999)", SqlDbType.NChar, 3999)]
    [InlineData("nchar(800)", SqlDbType.NChar, 800)]
    [InlineData("nchar(1)", SqlDbType.NChar, 1)]
    public void NChar(string expected, SqlDbType type, int precision)
    {
        Assert.Equal( expected, new ColumnDefinition("TestColumn", type, precision).ToSqlType() );
    }

    [Theory]
    [InlineData("char(1)", SqlDbType.Char, 0)]
    [InlineData("char(8000)", SqlDbType.Char, 8001)]
    [InlineData("char(8000)", SqlDbType.Char, 8000)]
    [InlineData("char(7999)", SqlDbType.Char, 7999)]
    [InlineData("char(800)", SqlDbType.Char, 800)]
    [InlineData("char(1)", SqlDbType.Char, 1)]
    public void Char(string expected, SqlDbType type, int precision)
    {
        Assert.Equal( expected, new ColumnDefinition("TestColumn", type, precision).ToSqlType() );
    }

    [Theory]
    [InlineData("varchar(max)", SqlDbType.VarChar, 0)]
    [InlineData("varchar(max)", SqlDbType.VarChar, 8001)]
    [InlineData("varchar(8000)", SqlDbType.VarChar, 8000)]
    [InlineData("varchar(7999)", SqlDbType.VarChar, 7999)]
    [InlineData("varchar(800)", SqlDbType.VarChar, 800)]
    [InlineData("varchar(1)", SqlDbType.VarChar, 1)]
    public void Varchar(string expected, SqlDbType type, int precision)
    {
        Assert.Equal( expected, new ColumnDefinition("TestColumn", type, precision).ToSqlType() );
    }

    [Theory]
    [InlineData("nvarchar(max)", SqlDbType.NVarChar, 0)]
    [InlineData("nvarchar(max)", SqlDbType.NVarChar, 4001)]
    [InlineData("nvarchar(4000)", SqlDbType.NVarChar, 4000)]
    [InlineData("nvarchar(3999)", SqlDbType.NVarChar, 3999)]
    [InlineData("nvarchar(400)", SqlDbType.NVarChar, 400)]
    [InlineData("nvarchar(1)", SqlDbType.NVarChar, 1)]
    public void Nvarchar(string expected, SqlDbType type, int precision)
    {
        Assert.Equal( expected, new ColumnDefinition("TestColumn", type, precision).ToSqlType() );
    }

    [Theory]
    [InlineData("tinyint", SqlDbType.Int, 1)]
    [InlineData("tinyint", SqlDbType.TinyInt, 0)]
    [InlineData("smallint", SqlDbType.Int, 2)]
    [InlineData("smallint", SqlDbType.SmallInt, 0)]
    [InlineData("int", SqlDbType.Int, 4)]
    [InlineData("bigint", SqlDbType.Int, 8)]
    [InlineData("bigint", SqlDbType.BigInt, 0)]
    public void Int(string expected, SqlDbType type, int precision)
    {
        //-2147483648L, 2147483647L
        Assert.Equal( expected, new ColumnDefinition("TestColumn", type, precision).ToSqlType() );
    }
}