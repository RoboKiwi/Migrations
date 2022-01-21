using Migrations.Engines.SqlServer;
using Migrations.Models;
using Xunit;

namespace Migrations.Tests.Engines.SqlServer
{
    public class SqlServerObjectNameGeneratorTests
    {
        [Theory]
        [InlineData("[tableName]", "tableName", null, null)]
        [InlineData("[dbo].[tableName]", "tableName", "dbo", null)]
        [InlineData("[dbName].[dbo].[tableName]", "tableName", "dbo", "dbName")]
        public void Generator(string expected, string name, string schema = null, string database = null)
        {
            var definition = new DummySchemaDefinition
            {
                Database = database, 
                Schema = schema, 
                Name = name
            };

            Assert.Equal(expected, ObjectNameGenerator.ToObjectName(definition));
        }

        class DummySchemaDefinition : SchemaDefinition
        {
        }
    }
}
