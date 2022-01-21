using System.Threading.Tasks;
using Migrations.Builder;
using Migrations.Engines.SqlServer;

namespace Migrations.Models.Graph;

public class TableNode : INode
{
    private readonly TableDefinition definition;
    private readonly SchemaOperation operation;

    public TableNode(TableDefinition definition, SchemaOperation operation)
    {
        this.definition = definition;
        this.operation = operation;
    }

    public Task Accept(INodeVisitor visitor)
    {
        switch (operation?.Action)
        {
            case SchemaAction.None:
                break;
            case SchemaAction.Create:
                visitor.Builder.AppendLine("CREATE TABLE " + definition.ToObjectName() + " (");
                break;
            case SchemaAction.Alter:
                visitor.Builder.AppendLine("ALTER TABLE " + definition.ToObjectName() + " (");
                break;
            case SchemaAction.Drop:
                visitor.Builder.AppendLine("DROP TABLE " + definition.ToObjectName() + ";");
                return Task.CompletedTask;
        }
            
        return Task.CompletedTask;
    }
}