using System;

namespace Integreat.Internal
{
    public static class Guard
    {
        public static void IsNotNull<T>(T value, string paramName) where T : class
        {
            if (value == null || (value is string && string.IsNullOrWhiteSpace(value as string)))
                throw new ArgumentNullException(paramName);
        }
    }
}
