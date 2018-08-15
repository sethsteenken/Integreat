using Integreat.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace Integreat
{
    public sealed class Process : IProcess
    {
        private readonly ILogger<Process> _logger;
        private readonly StringBuilder _resultBuilder;

        public Process(ILogger<Process> logger)
        {
            _logger = logger;
            Id = Guid.NewGuid();
            _resultBuilder = new StringBuilder();
        }

        public Guid Id { get; private set; }

        public string Execute(ProcessExecutionContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            // TODO - validate the context has executables on the Execution_Plan level

            foreach (var executableReference in context.Executables)
            {
                executableReference.Executable.Execute(new ExecutableContext(
                    context.IntegrationDirectory,
                    context.ExecutablesDirectory,
                    executableReference.Parameters,
                    executableReference.Timeout,
                    Log));
            }

            return _resultBuilder.ToString();
        }

        private void Log(string message)
        {
            _logger.LogInformation(message);
            _resultBuilder.AppendLine(LogFormatter.FormatMessage(message));
        }
    }
}
