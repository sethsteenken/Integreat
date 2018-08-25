using System.Collections.Generic;

namespace Integreat
{
    public sealed class ExecutionPlan : IExecutionPlan
    {
        private ExecutionPlan() { }

        public IReadOnlyList<ProcessExecutable> Executables { get; private set; }
        public string IntegrationDirectory { get; private set; }
        public string ExecutablesDirectory { get; private set; }
    }
}
