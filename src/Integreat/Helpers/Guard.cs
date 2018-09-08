﻿using System;

namespace Integreat
{
    public static class Guard
    {
        public static void IsNotNull<T>(T value, string paramName)
        {
            if (value == null || (value is string && string.IsNullOrWhiteSpace(value as string)))
                throw new ArgumentNullException(paramName);

            IsNotNull<T>(value, paramName, null);
        }

        public static void IsNotNull<T>(T value, string paramName, string message)
        {
            if (value == null || (value is string && string.IsNullOrWhiteSpace(value as string)))

            {
                if (string.IsNullOrWhiteSpace(message))
                    throw new ArgumentNullException(paramName);
                else
                    throw new ArgumentNullException(paramName, message);
            }  
        }
    }
}
