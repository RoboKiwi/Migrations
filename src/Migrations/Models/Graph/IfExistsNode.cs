using System.Threading.Tasks;

namespace Migrations.Models.Graph
{
    public class IfExistsNode : INode
    {
        private readonly SchemaDefinition definition;
        private readonly INode child;

        public IfExistsNode(SchemaDefinition definition, INode child)
        {
            this.definition = definition;
            this.child = child;
        }
        
        public async Task Accept(INodeVisitor visitor)
        {
            await visitor.BeginIfExists(definition);
            await visitor.Visit(child);
            await visitor.EndIfExists(definition);
        }
    }
}
