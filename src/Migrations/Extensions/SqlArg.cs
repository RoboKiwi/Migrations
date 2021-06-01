using System.Collections.Generic;
using System.Text;

namespace Migrations.Extensions
{
    public static class SqlArg
    {
        public static KeyValuePair<string, object> Name(object value)
        {
            return Create("@Name", value);
        }

        public static KeyValuePair<string, object> Create(string name, object value)
        {
            return new(name, value);
        }

        public static KeyValuePair<string, object> Id(object value)
        {
            return Create("@Id", value);
        }

        internal static string EscapeBracket(string s)
        {
            return EscapeString(s, ']');
        }

        public static string Bracketize(string s)
        {
            return "[" + EscapeString(s, ']') + "]";
        }

        internal static string EscapeString(string s, char escape)
        {
            if (s == null) return null;
            var stringBuilder = new StringBuilder();
            foreach (var ch in s)
            {
                stringBuilder.Append(ch);
                if (escape == ch) stringBuilder.Append(ch);
            }

            return stringBuilder.ToString();
        }
    }
}