using System;

namespace Integreat
{
    public class CSharpPluginProcessExecutableAdapter : ExecutableAdapterBase, IProcessExecutableAdapter
    {
        private readonly string _pluginExecutorAppPath;

        public CSharpPluginProcessExecutableAdapter(string pluginExecutorAppPath)
        {
            _pluginExecutorAppPath = pluginExecutorAppPath;
        }

        public ProcessExecutable Build(dynamic configurationValues)
        {
            Guard.IsNotNull(configurationValues, nameof(configurationValues));

            Type type = configurationValues.GetType();
            var properties = type.GetProperties();

            var executable = new CSharpPluginExecutable(
                GetPropertyValue(properties, configurationValues, "TypeName"), 
                GetPropertyValue(properties, configurationValues, "AssemblyName"), 
                _pluginExecutorAppPath);

            return new ProcessExecutable(executable, GetConfiguration(configurationValues, type, properties));
        }
    }
}
