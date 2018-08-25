using System;

namespace Integreat.Powershell
{
    public class PowershellProcessExecutableAdapter : ExecutableAdapterBase, IProcessExecutableAdapter
    {
        private readonly IFileStorage _fileStorage;

        public PowershellProcessExecutableAdapter(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public ProcessExecutable Build(dynamic configurationValues)
        {
            Guard.IsNotNull(configurationValues, nameof(configurationValues));

            Type type = configurationValues.GetType();
            var properties = type.GetProperties();

            var executable = new PowershellExecutable(_fileStorage, GetPropertyValue(properties, configurationValues, "File"));

            return new ProcessExecutable(executable, GetConfiguration(configurationValues, type, properties));
        }
    }
}
