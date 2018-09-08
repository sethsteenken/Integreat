using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions.Internal;
using System;
using System.Diagnostics;
using System.Text;

namespace Integreat
{
    internal class ConsoleLogger : ILogger
    {
        [ThreadStatic]
        private static StringBuilder _logBuilder;
        private readonly string _name;
        private const string _namePrefix = "Integreat.ConsoleLogger";

        public ConsoleLogger() : this (null)
        {

        }

        public ConsoleLogger(string name)
        {
            _name = string.IsNullOrWhiteSpace(name) ? _namePrefix : $"{_namePrefix}_{name}";
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NullScope.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            Guard.IsNotNull(formatter, nameof(formatter));

            var message = formatter(state, exception);

            if (!string.IsNullOrEmpty(message) || exception != null)
                WriteMessage(logLevel, _name, eventId.Id, message, exception);
        }

        public virtual void WriteMessage(LogLevel logLevel, string logName, int eventId, string message, Exception exception)
        {
            var logBuilder = _logBuilder;
            _logBuilder = null;

            if (logBuilder == null)
                logBuilder = new StringBuilder();

            logBuilder.Append(": ");
            logBuilder.Append(logName);
            logBuilder.Append("[");
            logBuilder.Append(eventId);
            logBuilder.AppendLine("]");

            if (!string.IsNullOrEmpty(message))
                logBuilder.AppendLine(message);

            if (exception != null)
                logBuilder.AppendLine(exception.ToString());

            if (logBuilder.Length > 0)
            {
                var result = logBuilder.ToString();
                Console.WriteLine(result);
                Debug.WriteLine(result);
            }
                
            logBuilder.Clear();
            if (logBuilder.Capacity > 1024)
                logBuilder.Capacity = 1024;

            _logBuilder = logBuilder;
        }

        private static string GetLogLevelString(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return "trce";
                case LogLevel.Debug:
                    return "dbug";
                case LogLevel.Information:
                    return "info";
                case LogLevel.Warning:
                    return "warn";
                case LogLevel.Error:
                    return "fail";
                case LogLevel.Critical:
                    return "crit";
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }
    }
}
