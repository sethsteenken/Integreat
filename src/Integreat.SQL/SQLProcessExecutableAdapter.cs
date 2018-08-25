using System;
using System.Data;

namespace Integreat.SQL
{
    public class SQLProcessExecutableAdapter : ExecutableAdapterBase, IProcessExecutableAdapter
    {
        private readonly IFileStorage _fileStorage;

        public SQLProcessExecutableAdapter(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public ProcessExecutable Build(dynamic configurationValues)
        {
            Guard.IsNotNull(configurationValues, nameof(configurationValues));

            Type type = configurationValues.GetType();
            var properties = type.GetProperties();

            var commandTypeValue = GetPropertyValue(properties, configurationValues, "SqlCommandType", required: false);
            if (!Enum.TryParse<CommandType>(commandTypeValue, out CommandType commandType))
                commandType = CommandType.Text;

            var executable = new SQLExecutable(
                _fileStorage, 
                GetPropertyValue(properties, configurationValues, "File"),
                GetPropertyValue(properties, configurationValues, "ConnectionString"),
                commandType);

            return new ProcessExecutable(executable, GetConfiguration(configurationValues, type, properties));
        }
    }
}
