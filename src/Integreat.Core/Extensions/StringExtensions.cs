using System;

namespace Integreat.Core
{
    internal static class StringExtensions
    {
        public static bool TryParse<T>(this string value, out T parsedValue)
        {
            parsedValue = default(T);

            if (string.IsNullOrWhiteSpace(value))
                return false;

            var type = typeof(T);

            if (type == typeof(string))
            {
                parsedValue = (T)(value as object);
                return true;
            }
            else if (type == typeof(bool))
            {
                if (bool.TryParse(value, out bool boolValue))
                {
                    parsedValue = (T)(boolValue as object);
                    return true;
                }
            }
            else if (type == typeof(int))
            {
                if (int.TryParse(value, out int intValue))
                {
                    parsedValue = (T)(intValue as object);
                    return true;
                }
            }
            else if (type == typeof(decimal))
            {
                if (decimal.TryParse(value, out decimal decimalValue))
                {
                    parsedValue = (T)(decimalValue as object);
                    return true;
                }
            }
            else if (type == typeof(double))
            {
                if (double.TryParse(value, out double doubleValue))
                {
                    parsedValue = (T)(doubleValue as object);
                    return true;
                }
            }
            else if (type == typeof(DateTime))
            {
                if (DateTime.TryParse(value, out DateTime dateValue))
                {
                    parsedValue = (T)(dateValue as object);
                    return true;
                }
            }
            else if (type == typeof(Guid))
            {
                if (Guid.TryParse(value, out Guid guidValue))
                {
                    parsedValue = (T)(guidValue as object);
                    return true;
                }
            }
            else
                throw new InvalidOperationException($"Type {type.FullName} is not a valid type to TryParse using {typeof(StringExtensions)}.");

            return false;
        }
    }
}
