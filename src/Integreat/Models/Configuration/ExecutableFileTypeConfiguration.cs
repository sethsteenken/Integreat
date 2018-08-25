using System.Collections.Generic;

namespace Integreat
{
    public class ExecutableFileTypeConfiguration : ExecutableConfiguration
    {
        public ExecutableFileTypeConfiguration(string type, int timeout, Dictionary<string, string> parameters, string filePath)
            : base(type, timeout, parameters)
        {
            File = filePath;
        }

        public string File { get; private set; }
    }
}
