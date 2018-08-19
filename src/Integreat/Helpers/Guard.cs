using System;

namespace Integreat
{
    public static class Guard
    {
        public static void IsNotNull<T>(T value, string paramName) where T : class
        {
            if (value == null || (value is string && string.IsNullOrWhiteSpace(value as string)))
                throw new ArgumentNullException(paramName);
        }

        public static void IsNotEmptyGuid(Guid value, string paramName)
        {
            if (value == Guid.Empty)
                throw new ArgumentNullException(paramName);
        }
    }
}
