using System;

namespace Integreat
{
    public class BatchProcessExecutableAdapter : ExecutableAdapterBase, IProcessExecutableAdapter
    {
        private readonly IFileStorage _fileStorage;

        public BatchProcessExecutableAdapter(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public ProcessExecutable Build(dynamic configurationValues)
        {
            Guard.IsNotNull(configurationValues, nameof(configurationValues));

            Type type = configurationValues.GetType();
            var properties = type.GetProperties();

            var executable = new BatchExecutable(_fileStorage, GetPropertyValue(properties, configurationValues, "File"));

            return new ProcessExecutable(executable, GetConfiguration(configurationValues, type, properties));
        }
    }
}
