using Integreat.Plugins;
using System;

namespace Integreat.PluginExecutor
{
    internal class PluginFactory
    {
        private readonly AssemblyTypeLoader _assemblyLoader;

        public PluginFactory(AssemblyTypeLoader assemblyLoader)
        {
            _assemblyLoader = assemblyLoader;
        }

        public IExecutionPlugin Create(string type, string assembly)
        {
            Type pluginType = _assemblyLoader.LoadType(type, assembly);
            return (IExecutionPlugin)Activator.CreateInstance(pluginType);
        }
    }
}
