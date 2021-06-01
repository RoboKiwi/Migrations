using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Migrations.Extensions
{
    public static class DbConnectionExtensions
    {
        public static Task<object> ExecuteScalarAsync(this DbConnection connection, string query,
            params KeyValuePair<string, object>[] parameters)
        {
            return ExecuteScalarAsync(connection, CancellationToken.None, query, parameters);
        }

        public static async Task<int> ExecuteBatchNonQueryAsync(this DbConnection connection, CancellationToken token,
            TextReader reader)
        {
            var rowsAffected = 0;

            var sb = new StringBuilder();

            using (var command = connection.CreateCommand())
            {
                for (var line = await reader.ReadLineAsync(); line != null; line = await reader.ReadLineAsync())
                    if (line.Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
                    {
                        if (sb.Length > 0)
                        {
                            command.CommandText = sb.ToString();
                            var affected = await command.ExecuteNonQueryAsync(token);
                            if (affected > 0) rowsAffected += affected;
                        }

                        sb.Clear();
                    }
                    else
                    {
                        sb.AppendLine(line);
                    }

                if (sb.Length > 0)
                {
                    command.CommandText = sb.ToString();
                    var affected = await command.ExecuteNonQueryAsync(token);
                    if (affected > 0) rowsAffected += affected;
                }
            }

            return rowsAffected;
        }

        public static async Task<int> ExecuteNonQueryAsync(this DbConnection connection, CancellationToken token,
            string query, params KeyValuePair<string, object>[] parameters)
        {
            using (var command = connection.CreateCommand(query, parameters))
            {
                return await command.ExecuteNonQueryAsync(token);
            }
        }

        public static async Task<object> ExecuteScalarAsync(this DbConnection connection, CancellationToken token,
            string query, params KeyValuePair<string, object>[] parameters)
        {
            using (var command = connection.CreateCommand(query, parameters))
            {
                return await command.ExecuteScalarAsync(token);
            }
        }

        public static async Task<T> ExecuteScalarAsync<T>(this DbConnection connection, CancellationToken token,
            string query, params KeyValuePair<string, object>[] parameters)
        {
            var value = await ExecuteScalarAsync(connection, token, query, parameters);
            return (T) value;
        }

        public static async Task<bool> ExistsAsync(this DbConnection connection, CancellationToken token, string query,
            params KeyValuePair<string, object>[] parameters)
        {
            using (var command = connection.CreateCommand(query, parameters))
            {
                return await command.ExistsAsync(token);
            }
        }

        public static DbCommand CreateCommand(this DbConnection connection, string query,
            params KeyValuePair<string, object>[] parameters)
        {
            return connection.CreateCommand().SetParameters(query, parameters);
        }

        /// <summary>
        ///     Returns <c>true</c> if the specified database exists; otherwise <c>false</c>.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="database"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<bool> DatabaseExists(this DbConnection connection, string database,
            CancellationToken token = default)
        {
            return await connection.ExistsAsync(token, "select [name] from [sys].[sysdatabases] where [name] = @Name",
                new KeyValuePair<string, object>("@Name", database));
        }

        public static async Task<bool> TableExistsAsync(this DbConnection connection, string table,
            CancellationToken token = default)
        {
            return await connection.ExistsAsync(token,
                "select table_name from information_schema.tables where table_name = @Name",
                new KeyValuePair<string, object>("@Name", table));
        }

        public static async Task<bool> ColumnExistsAsync(this DbConnection connection, string table, string column,
            CancellationToken token = default)
        {
            return await connection.ExistsAsync(token,
                "select column_name from information_schema.columns where table_name = @Name and column_name = @Column",
                SqlArg.Name(table), SqlArg.Create("@Column", column));
        }

        public static IDictionary<string, object> ExecuteRow(this DbConnection connection, string query,
            params KeyValuePair<string, object>[] parameters)
        {
            return ExecuteQuery(connection, query, parameters).SingleOrDefault();
        }

        public static async Task<IEnumerable<IDictionary<string, object>>> ExecuteQueryAsync(
            this DbConnection connection, string query, params KeyValuePair<string, object>[] parameters)
        {
            using (var command = connection.CreateCommand(query, parameters))
            {
                var results = new List<IDictionary<string, object>>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) results.Add(reader.ReadRow());
                }

                return results;
            }
        }

        public static async Task<IDictionary<string, object>> ExecuteRowAsync(this DbConnection connection,
            string query,
            params KeyValuePair<string, object>[] parameters)
        {
            var results = await ExecuteQueryAsync(connection, query, parameters);
            return results.SingleOrDefault();
        }

        public static IEnumerable<IDictionary<string, object>> ExecuteQuery(this DbConnection connection, string query,
            params KeyValuePair<string, object>[] parameters)
        {
            if (connection.State != ConnectionState.Open) connection.Open();

            using (var command = connection.CreateCommand(query, parameters))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    yield return reader.ReadRow();
            }
        }

        public static async Task<int> ExecuteNonQueryAsync(this DbConnection connection, string sql,
            params KeyValuePair<string, object>[] parameters)
        {
            var result = await ExecuteNonQueryAsync(connection, CancellationToken.None, sql, parameters);
            return result;
        }

        // public static async Task<SqlBulkCopy> BulkCopyAsync(this DbConnection connection, DataTable table, Version version)
        // {
        //     if( !(connection is SqlConnection sqlConnection)) throw new InvalidOperationException("Connection isn't a SqlConnection");
        //     var copy = new SqlBulkCopy(sqlConnection);
        //     copy.DestinationTableName = table.TableName;
        //     copy.AddColumnMappings(table, version);
        //     await copy.WriteToServerAsync(table);
        //     return copy;
        // }
    }
}