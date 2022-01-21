namespace Migrations.Models;

public abstract class SchemaDefinition
{
    /// <summary>
    /// The name of the schema object
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The schema that the object is owned by
    /// </summary>
    public string Schema { get; set; }

    /// <summary>
    /// The database that the object is contained in. Defaults
    /// to <c lang="csharp">null</c> to be implied from the current
    /// connection.
    /// </summary>
    public string Database { get; set; }
}