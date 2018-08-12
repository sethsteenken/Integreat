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

        // TODO - this will probably need to change - need params and timeout, etc
        public string Execute(IExecutionPlan executionPlan)
        {
            Guard.IsNotNull(executionPlan, nameof(executionPlan));

            if (executionPlan.Executables == null || executionPlan.Executables.Count == 0)
                throw new InvalidOperationException("There are no Executables registered in the Execution Plan.");

            foreach (var executable in executionPlan.Executables)
            {
                executable.Execute(new ExecutableContext(
                    executionPlan.IntegrationDirectory,
                    executionPlan.ExecutablesDirectory,
                    new ExecutableParameters(), // TODO - get params?
                    60, // TODO - timeout
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
