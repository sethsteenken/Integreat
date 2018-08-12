using System;

namespace Integreat.Core
{
    public static class LogFormatter
    {
        public static char DefaultLineSeparator = '*';
        public static string FailedResultCode = "FAIL";
        public static string SuccessfulResultCode = "SUCCESS";

        public static string GenerateHeadingLineSeparator(string headingText, char separator = default(char))
        {
            if (string.IsNullOrWhiteSpace(headingText))
                return string.Empty;

            if (separator == default(char))
                separator = DefaultLineSeparator;

            return new string(separator, headingText.Length);
        }

        public static string FormatMessage(string message)
        {
            return $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} -- {message}";
        }

        public static string GetResultCode(bool success)
        {
            return success ? SuccessfulResultCode : FailedResultCode;
        }
    }
}
