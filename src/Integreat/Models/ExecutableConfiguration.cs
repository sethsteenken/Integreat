using System.Collections.Generic;

namespace Integreat
{
    public sealed class ExecutableConfiguration
    {
        public ExecutableConfiguration(string type, int timeout, IDictionary<string, string> parameters)
        {
            Type = type;
            Timeout = timeout;
            Parameters = parameters ?? new Dictionary<string, string>();
        }

        public string Type { get; private set; }
        public int Timeout { get; private set; }
        public IDictionary<string, string> Parameters { get; private set; }
    }
}
