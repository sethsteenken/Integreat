using System;
using System.Reflection;

namespace Integreat
{
    public class BatchProcessExecutableAdapter : ProcessExecutableAdapter
    {
        private readonly IFileStorage _fileStorage;

        public BatchProcessExecutableAdapter(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        protected override IExecutable BuildExecutable(dynamic configurationValues, Type type, PropertyInfo[] properties)
        {
            return new BatchExecutable(
                _fileStorage, 
                GetPropertyValue(properties, configurationValues, "File"));
        }
    }
}
