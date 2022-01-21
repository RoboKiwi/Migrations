using Migrations.Engines.SqlServer;
using Xunit;

namespace Migrations.Tests.Engines.SqlServer;

public class SqlServerNodeVisitorTests
{
    private readonly SqlServerNodeVisitor visitor;

    public SqlServerNodeVisitorTests()
    {
        visitor = new SqlServerNodeVisitor();
    }

    [Fact]
    public void BeginIfExists()
    {
        visitor.BeginIfExists(new DummySchemaDefinition("db", "dbo", "TestTable"));
        Assert.Equal(@"IF EXISTS (SELECT OBJECT_ID(N'[db].[dbo].[TestTable]'))
BEGIN
", visitor.Builder.ToString());
    }

    [Fact]
    public void EndIfExists()
    {
        visitor.EndIfExists(new DummySchemaDefinition("db", "dbo", "TestTable"));
        Assert.Equal(@"END
", visitor.Builder.ToString());
    }
}