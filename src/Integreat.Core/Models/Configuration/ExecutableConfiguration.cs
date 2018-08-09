using System.Collections.Generic;

namespace Integreat.Core
{
    internal sealed class ExecutableConfiguration
    {
        public ExecutableTypeOption Type { get; private set; }
        public string File { get; private set; }
        public int Timeout { get; private set; }
        public string TypeName { get; private set; }
        public string AssemblyName { get; private set; }
        public string ConnectionString { get; private set; }
        public string SqlCommandType { get; private set; }
        public Dictionary<string, object> Parameters { get; private set; }
    }
}
