using System;
using System.Collections.Generic;
using System.Linq;

namespace Integreat
{
    public class ExecutionPlan : IExecutionPlan
    {
        private ExecutionPlan() { }

        // TODO
        //internal ExecutionPlan(IProcess process, IFileStorage fileStorage, ExecutionPlanReference reference, string integrationDirectory, string executablesDirectory)
        //{
        //    Executables = reference?.Executables.Select(x => Executable.Build(process, fileStorage, x)).ToList() ?? new List<Executable>();

        //    IntegrationDirectory = string.IsNullOrWhiteSpace(integrationDirectory) ? throw new ArgumentNullException(nameof(integrationDirectory)) : integrationDirectory.Trim();
        //    ExecutablesDirectory = string.IsNullOrWhiteSpace(executablesDirectory) ? throw new ArgumentNullException(nameof(executablesDirectory)) : executablesDirectory.Trim();
        //}

        public IReadOnlyList<IExecutable> Executables { get; private set; }
        public string IntegrationDirectory { get; private set; }
        public string ExecutablesDirectory { get; private set; }

        public void Execute()
        {
            if (Executables == null || Executables.Count == 0)
                throw new InvalidOperationException("There are no Executables registered in the Execution Plan.");

            foreach (var executable in Executables)
            {
                executable.Execute(new ExecutionContext(IntegrationDirectory, ExecutablesDirectory));
            }
        }
    }
}
