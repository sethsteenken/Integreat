using System;
using System.Data;
using System.Reflection;

namespace Integreat.SQL
{
    public class SQLProcessExecutableAdapter : ProcessExecutableAdapter<SQLExecutable>
    {
        private readonly IFileStorage _fileStorage;

        public SQLProcessExecutableAdapter(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        protected override SQLExecutable BuildExecutable(dynamic configurationValues, Type type, PropertyInfo[] properties)
        {
            var commandTypeValue = GetPropertyValue(properties, configurationValues, "SqlCommandType", required: false);
            if (!Enum.TryParse<CommandType>(commandTypeValue, out CommandType commandType))
                commandType = CommandType.Text;

            return new SQLExecutable(
                _fileStorage,
                GetPropertyValue(properties, configurationValues, "File"),
                GetPropertyValue(properties, configurationValues, "ConnectionString"),
                commandType);
        }
    }
}
