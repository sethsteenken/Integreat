﻿using System;

namespace Integreat
{
    public static class Guard
    {
        public static void IsNotNull<T>(T value, string paramName)
        {
            if (value == null || (value is string && string.IsNullOrWhiteSpace(value as string)))
                throw new ArgumentNullException(paramName);
        }
    }
}
