using System.Collections.Generic;

namespace Integreat
{
    internal sealed class ExecutablePlanConfiguration
    {
        public IReadOnlyList<ExecutableConfiguration> Executables { get; private set; }
        public string IntegrationDirectory { get; private set; }
        public string ExecutablesDirectory { get; private set; }
    }
}
