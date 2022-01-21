using System;
using System.Threading.Tasks;
using Migrations.Builder;
using Migrations.Models;
using Migrations.Models.Graph;

namespace Migrations.Engines.SqlServer;

public class SqlServerNodeVisitor : INodeVisitor
{
    public ISqlBuilder Builder { get; } = new SqlServerSqlBuilder();

    public Task Visit(INode node)
    {
        return node.Accept(this);
    }

    public Task BeginIfExists(SchemaDefinition definition)
    {
        var fqn = ObjectNameGenerator.ToObjectName(definition);
        Builder.AppendLine($"IF EXISTS (SELECT OBJECT_ID(N'{fqn}'))");
        Builder.AppendLine("BEGIN");
        return Task.CompletedTask;
    }

    public Task EndIfExists(SchemaDefinition definition)
    {
        Builder.AppendLine("END");
        return Task.CompletedTask;
    }

    public Task Column(ColumnDefinition definition, SchemaOperation operation)
    {
        switch (operation?.Action)
        {
            case SchemaAction.None:
                break;
            case SchemaAction.Create:
                Builder.Append("ADD " + definition.ToObjectName());

                Builder.Append(" " + definition.ToSqlType());

                if (definition.IsNullable == true) Builder.Append(" NULL");

                Builder.AppendLine(";");
                break;

            default:
                throw new NotImplementedException();
        }

        return Task.CompletedTask;
    }
}