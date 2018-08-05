namespace Integreat
{
    public static class StringExtensions
    {
        /// <summary>
        /// If <paramref name="value"/> contains a space, value is returned inside escaped double quotations;
        /// </summary>
        /// <param name="value"></param>
        /// <param name="force">Force escaped double quotations on the value regardless if value contains spaces.</param>
        /// <returns></returns>
        public static string QuoteIfSpaces(this string value, bool force = false)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;

            if (value.Contains(" ") || force)
                return $"\"{value}\"";
            else
                return value;
        }
    }
}
