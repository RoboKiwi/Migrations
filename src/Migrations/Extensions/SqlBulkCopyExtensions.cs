namespace Migrations.Extensions
{
    public static class SqlBulkCopyExtensions
    {
        // /// <summary>
        // /// Automatically generates bulk copy column mappings by excluding columns
        // /// that aren't in the detected database version.
        // /// </summary>
        // /// <param name="bulk"></param>
        // /// <param name="table"></param>
        // /// <param name="version"></param>
        // public static void AddColumnMappings(this SqlBulkCopy bulk, DataTable table, Version version)
        // {
        //     var type = table.GetType();
        //
        //     var provider = new AssociatedMetadataTypeTypeDescriptionProvider(type);
        //
        //     if (TypeDescriptor.GetProvider(type).GetType() != typeof(AssociatedMetadataTypeTypeDescriptionProvider))
        //     {
        //         TypeDescriptor.AddProvider(provider, type);
        //     }
        //     
        //     var properties = TypeDescriptor.GetProperties(type);
        //     
        //     foreach (DataColumn column in table.Columns)
        //     {
        //         var property = properties[column.ColumnName + "Column"];
        //
        //         if (Version.TryParse(property.Category, out var columnVersion))
        //         {
        //             if (columnVersion > version) continue;
        //         }
        //
        //         bulk.ColumnMappings.Add(column.ColumnName, column.ColumnName);
        //     }
        // }
    }
}