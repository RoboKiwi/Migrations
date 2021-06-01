using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Migrations.Extensions
{
    static class DbDataReaderExtensions
    {
        /// <summary>
        ///     Returns the current row values as a dictionary.
        /// </summary>
        /// <remarks>All the row values are returned, with the name of the field as the key.</remarks>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ReadRow(this DbDataReader reader)
        {
            return Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue);
        }
    }
}