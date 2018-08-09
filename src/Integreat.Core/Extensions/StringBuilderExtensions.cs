using System.Text;

namespace Integreat.Core
{
    internal static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends <paramref name="key"/> and <paramref name="value"/> separated by "=".
        /// Key and Value are trimmed and quoted if either includes spaces.
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="appendSpace"></param>
        public static void AppendKeyValueArgument(this StringBuilder sb, string key, string value, bool appendSpace = true)
        {
            if (sb == null || string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                return;

            sb.Append(key.Trim().QuoteIfSpaces());
            sb.Append("=");
            sb.Append(value.Trim().QuoteIfSpaces());

            if (appendSpace)
                sb.Append(" ");
        }
    }
}
