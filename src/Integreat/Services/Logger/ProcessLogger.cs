using Integreat.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace Integreat
{
    public class ProcessLogger : IProcessLogger
    {
        private readonly ILogger<ProcessLogger> _logger;
        private readonly StringBuilder _resultBuilder;

        public ProcessLogger(ILogger<ProcessLogger> logger)
        {
            _logger = logger;
            _resultBuilder = new StringBuilder();
        }

        public void LogError(Exception ex)
        {
            _resultBuilder.AppendLine("** FAILURE **");
            _resultBuilder.AppendLine($"Process EXCEPTION: {ex}");

            _logger.LogError(ex, ex.Message);
        }

        public void LogInfo(string message)
        {
            _logger.LogInformation(message);
            _resultBuilder.AppendLine(LogFormatter.FormatMessage(message));
        }

        public override string ToString()
        {
            return _resultBuilder.ToString();
        }
    }
}
