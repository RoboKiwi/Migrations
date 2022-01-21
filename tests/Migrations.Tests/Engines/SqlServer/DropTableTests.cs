using Migrations.Builder;
using Migrations.Engines.SqlServer;
using Migrations.Models;
using Migrations.Models.Graph;
using Xunit;

namespace Migrations.Tests.Engines.SqlServer;

public class DropTableTests
{
    [Fact]
    public void DropTableIfExists()
    {

        // Drop table operation
        var operation = new SchemaOperation
        {
            Action = SchemaAction.Drop,
            Condition = SchemaCondition.IfExists
        };

        var generator = new SqlServerNodeVisitor();

        var definition = new TableDefinition("TestTable", "dbo");
        
        var node = new TableNode(definition, operation);

        var existsNode = new IfExistsNode(definition, node);

        generator.Visit(existsNode);

        Assert.Equal(@"IF EXISTS (SELECT OBJECT_ID(N'[dbo].[TestTable]'))
BEGIN
DROP TABLE [dbo].[TestTable];
END
", generator.Builder.ToString());
    }

    [Fact]
    public void DropTableIfAnotherTableExists()
    {

        // Drop table operation
        var operation = new SchemaOperation
        {
            Action = SchemaAction.Drop,
            Condition = SchemaCondition.IfExists
        };

        var generator = new SqlServerNodeVisitor();

        var definition = new TableDefinition("TableToDrop", "dbo");

        var node = new TableNode(definition, operation);

        var existsNode = new IfExistsNode(new TableDefinition("TableToCheck", "dbo"), node);

        generator.Visit(existsNode);

        Assert.Equal(@"IF EXISTS (SELECT OBJECT_ID(N'[dbo].[TableToCheck]'))
BEGIN
DROP TABLE [dbo].[TableToDrop];
END
", generator.Builder.ToString());
    }
}