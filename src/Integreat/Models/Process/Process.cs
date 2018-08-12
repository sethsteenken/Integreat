using Integreat.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integreat
{
    public sealed class Process : IProcess
    {
        private readonly IReadOnlyList<IExecutable> _executables;
        private readonly ILogger<Process> _logger;
        private readonly StringBuilder _resultBuilder;

        public Process(IReadOnlyList<IExecutable> executables, ILogger<Process> logger)
        {
            _executables = executables;
            _logger = logger;
            Id = Guid.NewGuid();
            _resultBuilder = new StringBuilder();
        }

        public Guid Id { get; private set; }

        public string Result => _resultBuilder.ToString();

        public void Execute()
        {
            if (_executables == null || _executables.Count == 0)
                throw new InvalidOperationException("There are no Executables registered in the Execution Plan.");

            foreach (var executable in _executables)
            {
                // TODO
                executable.Execute(new ExecutableContext("integrationDir", "executablesDir", new ExecutableParameters(), 60, Log));
            }
        }

        private void Log(string message)
        {
            _logger.LogInformation(message);
            _resultBuilder.AppendLine(LogFormatter.FormatMessage(message));
        }
    }
}
