using System.Collections.Generic;

namespace Integreat
{
    public class ExecutableConfiguration
    {
        public ExecutableConfiguration(string type, int timeout, Dictionary<string, string> parameters)
        {
            Type = type;
            Timeout = timeout;
            Parameters = parameters;
        }

        public string Type { get; private set; }
        public int Timeout { get; private set; }
        public Dictionary<string, string> Parameters { get; private set; }
    }
}
