namespace Integreat.Core
{
    public static class LogFormatter
    {
        public static char DefaultLineSeparator = '*';

        public static string GenerateHeadingLineSeparator(string headingText, char separator = default(char))
        {
            if (string.IsNullOrWhiteSpace(headingText))
                return string.Empty;

            if (separator == default(char))
                separator = DefaultLineSeparator;

            return new string(separator, headingText.Length);
        }
    }
}
