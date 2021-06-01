using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Migrations.Extensions
{
    public static class DbCommandExtensions
    {
        public static DbCommand SetParameters(this DbCommand command, string query,
            params KeyValuePair<string, object>[] parameters)
        {
            command.CommandText = query;
            foreach (var pair in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.Value = pair.Value;
                parameter.ParameterName = pair.Key;
                command.Parameters.Add(parameter);
            }

            return command;
        }

        public static async Task<bool> ExistsAsync(this DbCommand command, CancellationToken token = default)
        {
            using (var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow, token))
            {
                return reader.Read();
            }
        }
    }
}