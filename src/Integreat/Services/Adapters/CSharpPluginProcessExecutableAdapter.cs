using System;
using System.Reflection;

namespace Integreat
{
    public class CSharpPluginProcessExecutableAdapter : ProcessExecutableAdapter
    {
        private readonly string _pluginExecutorAppPath;

        public CSharpPluginProcessExecutableAdapter(string pluginExecutorAppPath)
        {
            _pluginExecutorAppPath = pluginExecutorAppPath;
        }

        protected override IExecutable BuildExecutable(dynamic configurationValues, Type type, PropertyInfo[] properties)
        {
            return new CSharpPluginExecutable(
                GetPropertyValue(properties, configurationValues, "TypeName"),
                GetPropertyValue(properties, configurationValues, "AssemblyName"),
                _pluginExecutorAppPath);
        }
    }
}
