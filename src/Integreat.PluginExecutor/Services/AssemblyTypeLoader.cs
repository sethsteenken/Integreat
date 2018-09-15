using Integreat.Plugins;
using System;
using System.Linq;
using System.Reflection;

namespace Integreat.PluginExecutor
{
    internal class AssemblyTypeLoader
    {
        private readonly string _libraryPath;
        private readonly AppDomain _appDomain;

        public AssemblyTypeLoader(string libraryPath, AppDomain appDomain)
        {
            _libraryPath = string.IsNullOrWhiteSpace(libraryPath) ? throw new ArgumentNullException(nameof(libraryPath)) : libraryPath.Trim();
            _appDomain = appDomain ?? throw new ArgumentNullException(nameof(appDomain));
        }

        public Type LoadType(string typeName, string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentNullException(nameof(typeName));
            if (string.IsNullOrWhiteSpace(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName));

            var assembly = Assembly.LoadFrom(_libraryPath);
            _appDomain.Load(assembly.GetName());

            Validate(typeName, assemblyName, out Type type);
            return type;
        }

        private void Validate(string typeName, string assemblyName, out Type type)
        {
            type = _appDomain.GetAssemblies().Where(x => x.GetType(typeName) != null).FirstOrDefault()?.GetType(typeName);
            if (type == null)
                throw new TypeLoadException($"Type '{typeName}' not found in assembly {assemblyName}.");

            if (!type.GetInterfaces().Contains(typeof(IExecutionPlugin)))
                throw new TypeLoadException($"Type {type.FullName} must implement {typeof(IExecutionPlugin).FullName}.");

            if (type.GetConstructor(Type.EmptyTypes) == null)
                throw new TypeLoadException($"Type {type.FullName} must have a default constructor.");
        }
    }
}
