using System;
using System.Reflection;

namespace Integreat.Batch
{
    public class BatchProcessExecutableAdapter : ProcessExecutableAdapter<BatchExecutable>
    {
        private readonly IFileStorage _fileStorage;

        public BatchProcessExecutableAdapter(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        protected override BatchExecutable BuildExecutable(dynamic configurationValues, Type type, PropertyInfo[] properties)
        {
            return new BatchExecutable(
                _fileStorage, 
                GetPropertyValue(properties, configurationValues, "File"));
        }
    }
}
