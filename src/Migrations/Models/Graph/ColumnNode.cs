using System.Threading.Tasks;

namespace Migrations.Models.Graph;

public class ColumnNode : INode
{
    private readonly ColumnDefinition definition;
    private readonly SchemaOperation operation;

    public ColumnNode(ColumnDefinition definition, SchemaOperation operation)
    {
        this.definition = definition;
        this.operation = operation;
    }

    public Task Accept(INodeVisitor visitor)
    {
        return visitor.Column(definition, operation);
    }
}