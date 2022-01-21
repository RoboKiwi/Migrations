using System.Threading.Tasks;
using Migrations.Engines.SqlServer;

namespace Migrations.Models.Graph;

public interface INodeVisitor
{
    ISqlBuilder Builder { get; }

    Task Visit(INode node);

    Task BeginIfExists(SchemaDefinition definition);

    Task EndIfExists(SchemaDefinition definition);

    Task BeginIfNotExists(SchemaDefinition definition);

    Task EndIfNotExists(SchemaDefinition definition);

    Task Column(ColumnDefinition definition, SchemaOperation operation);
}