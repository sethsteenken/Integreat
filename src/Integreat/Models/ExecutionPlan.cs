using System.Collections.Generic;

namespace Integreat
{
    public sealed class ExecutionPlan : IExecutionPlan
    {
        public ExecutionPlan(IReadOnlyList<ProcessExecutable> executables, string integrationDirectory, string executablesDirectory)
        {
            Executables = executables;
            IntegrationDirectory = integrationDirectory;
            ExecutablesDirectory = executablesDirectory;
        }

        public IReadOnlyList<ProcessExecutable> Executables { get; private set; }
        public string IntegrationDirectory { get; private set; }
        public string ExecutablesDirectory { get; private set; }
    }
}
