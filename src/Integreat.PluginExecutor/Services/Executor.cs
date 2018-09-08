using Integreat.Core;
using System;
using System.Text;

namespace Integreat.PluginExecutor
{
    internal class Executor
    {
        private readonly ExecutorSettings _settings;
        private readonly PluginFactory _pluginFactory;
        private readonly StringBuilder _logBuilder;

        public Executor(ExecutorSettings settings, PluginFactory pluginFactory)
        {
            _settings = settings;
            _pluginFactory = pluginFactory;
            _logBuilder = new StringBuilder();
        }

        public string ExecutePlugin()
        {
            bool success = false;

            try
            {
                var plugin = _pluginFactory.Create(_settings.TypeName, _settings.AssemblyName);
                if (plugin == null)
                    throw new ArgumentNullException("Plugin instance empty.");

                var context = new ExecutableContext(
                                    _settings.IntegrationDirectory,
                                    _settings.ExecutablesDirectory,
                                    _settings.Parameters,
                                    0, // default timeout to 0 as the timeout value will be used outside the scope of the plugin
                                    Log);

                plugin.Execute(context);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                Log($"Exception: {ex}");
            }

            return Result(success);
        }

        private void Log(string message)
        {
            _logBuilder.AppendLine(LogFormatter.FormatMessage(message));
        }

        private string Result(bool success)
        {
            return string.Concat(
                LogFormatter.GetResultCode(success),
                Environment.NewLine,
                _logBuilder.ToString());
        }
    }
}
