using Migrations.Builder;

namespace Migrations.Models;

/// <summary>
/// A schema operation is applied as an action if
/// the specified condition is met.
/// </summary>
public class SchemaOperation
{
    public SchemaOperation(SchemaAction action, SchemaCondition condition)
    {
        Action = action;
        Condition = condition;
    }

    public SchemaOperation(SchemaAction action)
    {
        Action = action;
    }

    public SchemaOperation()
    {
    }

    /// <summary>
    /// The action to apply to the operation, if the <see cref="Condition"/> is met.
    /// </summary>
    public SchemaAction Action { get; set; }

    /// <summary>
    /// The condition to be met for the operation to be applied.
    /// </summary>
    public SchemaCondition Condition { get; set; }

    public static SchemaOperation Create()
    {
        return new SchemaOperation(SchemaAction.Create);
    }
}