using System.Collections.Generic;
using System.Text;

namespace Migrations.Tests
{
    public static class StringCasingHelper
    {
        /// <summary>
        /// Converts to snake case e.g. from <c>"SnakeCase"</c> to <c>"snake_case"</c>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSnakeCase(string value) => ToSeparatedCase(value, '_');

        /// <summary>
        /// Converts to kebab case e.g. from <c>"SnakeCase"</c> to <c>"snake-case"</c>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToKebabCase(string value) => ToSeparatedCase(value, '-');

        enum CaseState
        {
            Start,
            Lower,
            Upper,
            NewWord
        }

        /// <summary>
        /// Converts camel case or pascal case to a delimited case with
        /// a specified separator
        /// </summary>
        /// <remarks>Based on Newtonsoft.Json.Utilities.StringCasingHelper.ToSeparatedCase</remarks>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToSeparatedCase(string value, char separator)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var sb = new StringBuilder();
            var state = CaseState.Start;

            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] == ' ')
                {
                    if (state != CaseState.Start) state = CaseState.NewWord;
                }
                else if (char.IsUpper(value[i]))
                {
                    switch (state)
                    {
                        case CaseState.Upper:
                            var hasNext = i + 1 < value.Length;
                            if (i > 0 && hasNext)
                            {
                                var nextChar = value[i + 1];
                                if (!char.IsUpper(nextChar) && nextChar != separator)
                                {
                                    sb.Append(separator);
                                }
                            }
                            break;
                        case CaseState.Lower:
                        case CaseState.NewWord:
                            sb.Append(separator);
                            break;
                    }

                    var c = char.ToLowerInvariant(value[i]);

                    sb.Append(c);

                    state = CaseState.Upper;
                }
                else if (value[i] == separator)
                {
                    sb.Append(separator);
                    state = CaseState.Start;
                }
                else
                {
                    if (state == CaseState.NewWord) sb.Append(separator);
                    sb.Append(value[i]);
                    state = CaseState.Lower;
                }
            }

            return sb.ToString();
        }

        public static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
            {
                return s;
            }

            char[] chars = s.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                {
                    break;
                }

                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    // if the next character is a space, which is not considered uppercase 
                    // (otherwise we wouldn't be here...)
                    // we want to ensure that the following:
                    // 'FOO bar' is rewritten as 'foo bar', and not as 'foO bar'
                    // The code was written in such a way that the first word in uppercase
                    // ends when if finds an uppercase letter followed by a lowercase letter.
                    // now a ' ' (space, (char)32) is considered not upper
                    // but in that case we still want our current character to become lowercase
                    if (char.IsSeparator(chars[i + 1]))
                    {
                        chars[i] = char.ToLower(chars[i]);
                    }

                    break;
                }

                chars[i] = char.ToLower(chars[i]);
            }

            return new string(chars);
        }

        internal static IEnumerable<char> ToPascalCase(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            var state = CaseState.NewWord;
            var sb = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                if (!char.IsLetterOrDigit(c))
                {
                    state = CaseState.NewWord;
                    continue;
                }

                if (state == CaseState.NewWord)
                {
                    state = CaseState.Upper;
                    sb.Append(char.ToUpper(c));
                    continue;
                }

                if (char.IsUpper(c))
                {
                    // If we're already in upper case, then this might still be the
                    // same word; if we peek the next character and it's lower case, then
                    // this is actually the start of a new word.

                    if( state == CaseState.Upper || state == CaseState.NewWord)
                    {
                        if(i < s.Length - 1 && char.IsLower(s[i + 1]))
                        {
                            // This is the start of a new word
                            state = CaseState.Upper;
                            sb.Append(c);
                        }
                        else
                        {
                            // Still in the same all-uppercase word
                            state = CaseState.Upper;
                            sb.Append(char.ToLower(c));
                        }
                    }
                    else
                    {
                        state = CaseState.Upper;
                        sb.Append(c);
                    }
                    
                    continue;
                }
                                
                state = CaseState.Lower;
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
