namespace Migrations.Engines.SqlServer;

/// <summary>
/// Builds up SQL scripts and handles database engine specifics
/// like escaping and delimiting.
/// </summary>
public interface ISqlBuilder
{
    ISqlBuilder AppendLine(string sql);

    ISqlBuilder Append(string sql);
}