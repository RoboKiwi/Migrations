using System.Data;
using System.Threading.Tasks;
using Migrations.Engines.SqlServer;
using Migrations.Models;
using Migrations.Models.Graph;
using Xunit;

namespace Migrations.Tests.Engines.SqlServer;

public class AddColumnTests
{
    [Fact]
    public async Task AddColumn()
    {
        var operation = SchemaOperation.Create();
        
        var definition = new ColumnDefinition("NewColumn");
        definition.SqlType = SqlDbType.NVarChar;

        var generator = new SqlServerNodeVisitor();
        
        var node = new ColumnNode(definition, operation);

        await generator.Visit(node);

        Assert.Equal(@"ADD [NewColumn] nvarchar(max);
", generator.Builder.ToString());
    }

    [Fact]
    public async Task AddColumnWithPrecision()
    {
        var operation = SchemaOperation.Create();

        var definition = new ColumnDefinition("NewColumn");
        definition.SqlType = SqlDbType.NVarChar;
        definition.Precision = 500;

        var generator = new SqlServerNodeVisitor();

        var node = new ColumnNode(definition, operation);

        await generator.Visit(node);

        Assert.Equal(@"ADD [NewColumn] nvarchar(500);
", generator.Builder.ToString());
    }


    [Fact]
    public async Task AddNullableColumn()
    {
        var operation = SchemaOperation.Create();

        var definition = new ColumnDefinition("NewColumn");
        definition.SqlType = SqlDbType.NVarChar;
        definition.IsNullable = true;

        var generator = new SqlServerNodeVisitor();

        var node = new ColumnNode(definition, operation);

        await generator.Visit(node);

        Assert.Equal(@"ADD [NewColumn] nvarchar(max) NULL;
", generator.Builder.ToString());
    }

}