﻿using Integreat.Core;

namespace Integreat
{
    internal static class ExecutableContextExtensions
    {
        public static void LogStartupHeading(this ExecutableContext context, string executableName)
        {
            string headingMessage = $"EXECUTABLE - {executableName}";
            context.Log(LogFormatter.GenerateHeadingLineSeparator(headingMessage));
            context.Log(headingMessage);
            context.Log(LogFormatter.GenerateHeadingLineSeparator(headingMessage));
        }
    }
}
