using System;
using Integreat.Core;

namespace Integreat.PluginExecutor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Executor executor = BuildExecutor(args);
                var result = executor.ExecutePlugin();

                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Concat(
                    LogFormatter.FailedResultCode,
                    Environment.NewLine,
                    $"Exception: {ex}"));
            }
        }

        static Executor BuildExecutor(string[] args)
        {
            var settings = new ExecutorSettings(args);
            AssemblyTypeLoader assemblyLoader = new AssemblyTypeLoader(settings.LibraryPath, AppDomain.CurrentDomain);
            PluginFactory pluginFactory = new PluginFactory(assemblyLoader);

            return new Executor(settings, pluginFactory);
        }
    }
}
