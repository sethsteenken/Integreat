using System.Collections.Generic;

namespace Integreat
{
    public class ExecutableConfiguration
    {
        public string Type { get; private set; }
        public int Timeout { get; private set; }
        public Dictionary<string, string> Parameters { get; private set; }
    }
}
