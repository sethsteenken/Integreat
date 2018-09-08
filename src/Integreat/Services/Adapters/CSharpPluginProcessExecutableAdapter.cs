using System;
using System.Reflection;

namespace Integreat
{
    public class CSharpPluginProcessExecutableAdapter : ProcessExecutableAdapter<CSharpPluginExecutable>
    {
        private readonly string _pluginExecutorAppPath;

        public CSharpPluginProcessExecutableAdapter(string pluginExecutorAppPath)
        {
            _pluginExecutorAppPath = pluginExecutorAppPath;
        }

        protected override CSharpPluginExecutable BuildExecutable(dynamic configurationValues, Type type, PropertyInfo[] properties)
        {
            return new CSharpPluginExecutable(
                GetPropertyValue(properties, configurationValues, "TypeName"),
                GetPropertyValue(properties, configurationValues, "AssemblyName"),
                _pluginExecutorAppPath);
        }
    }
}
