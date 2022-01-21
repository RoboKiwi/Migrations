using System.Text;

namespace Migrations.Engines.SqlServer
{
    internal class SqlServerSqlBuilder : ISqlBuilder
    {
        private readonly StringBuilder sb;

        public SqlServerSqlBuilder()
        {
            sb = new StringBuilder();
        }

        public ISqlBuilder AppendLine(string sql)
        {
            sb.AppendLine(sql);
            return this;
        }

        public ISqlBuilder Append(string sql)
        {
            sb.Append(sql);
            return this;
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
