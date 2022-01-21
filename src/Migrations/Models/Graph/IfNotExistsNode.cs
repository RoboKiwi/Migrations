using System.Threading.Tasks;

namespace Migrations.Models.Graph;

public class IfNotExistsNode : INode
{
    private readonly SchemaDefinition definition;
    private readonly INode child;

    public IfNotExistsNode(SchemaDefinition definition, INode child)
    {
        this.definition = definition;
        this.child = child;
    }
        
    public async Task Accept(INodeVisitor visitor)
    {
        await visitor.BeginIfNotExists(definition);
        await visitor.Visit(child);
        await visitor.EndIfNotExists(definition);
    }
}