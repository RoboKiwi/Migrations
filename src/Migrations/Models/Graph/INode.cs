using System.Threading.Tasks;

namespace Migrations.Models.Graph;

public interface INode
{
    public Task Accept(INodeVisitor visitor);
}