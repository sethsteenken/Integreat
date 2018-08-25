using System.Collections.Generic;

namespace Integreat.SQL
{
    public class ExecutableSQLTypeConfiguration : ExecutableFileTypeConfiguration
    {
        public ExecutableSQLTypeConfiguration(string type, int timeout, Dictionary<string, string> parameters, string filePath, string connString, string commandType)
            : base (type, timeout, parameters, filePath)
        {
            ConnectionString = connString;
            SqlCommandType = commandType;
        }

        public string ConnectionString { get; private set; }
        public string SqlCommandType { get; private set; }
    }
}
