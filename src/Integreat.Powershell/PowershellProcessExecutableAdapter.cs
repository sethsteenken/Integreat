using System;
using System.Reflection;

namespace Integreat.Powershell
{
    public class PowershellProcessExecutableAdapter : ProcessExecutableAdapter
    {
        private readonly IFileStorage _fileStorage;

        public PowershellProcessExecutableAdapter(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        protected override IExecutable BuildExecutable(dynamic configurationValues, Type type, PropertyInfo[] properties)
        {
            return new PowershellExecutable(
                _fileStorage, 
                GetPropertyValue(properties, configurationValues, "File"));
        }
    }
}
