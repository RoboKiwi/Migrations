using Migrations.Models;

namespace Migrations.Tests.Engines.SqlServer;

public class DummySchemaDefinition : SchemaDefinition
{
    public DummySchemaDefinition(string database, string schema, string name)
    {
        Database = database;
        Schema = schema;
        Name = name;
    }
}