using System.Collections.Generic;

namespace Integreat
{
    public class ExecutableCSharpTypeConfiguration : ExecutableConfiguration
    {
        public ExecutableCSharpTypeConfiguration(string type, int timeout, Dictionary<string, string> parameters, string typeName, string assemblyName)
            : base (type, timeout, parameters)
        {
            TypeName = typeName;
            AssemblyName = AssemblyName;
        }

        public string TypeName { get; private set; }
        public string AssemblyName { get; private set; }
    }
}
