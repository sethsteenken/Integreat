using System;

namespace Integreat.Plugins
{
    /// <summary>
    /// Information passed to each executable. Typically includes the associated directories of the given execution process.
    /// </summary>
    public sealed class ExecutableContext
    {
        private readonly Action<string> _logAction;

        public ExecutableContext(string integrationDirectory, string executablesDirectory, ExecutableParameters parameters, int timeout, Action<string> logAction)
        {
            IntegrationDirectory = string.IsNullOrWhiteSpace(integrationDirectory) ? throw new ArgumentNullException(nameof(integrationDirectory)) : integrationDirectory.Trim();
            ExecutablesDirectory = string.IsNullOrWhiteSpace(executablesDirectory) ? throw new ArgumentNullException(nameof(executablesDirectory)) : executablesDirectory.Trim();
            Parameters = parameters ?? new ExecutableParameters();
            Timeout = timeout <= 0 ? 60 : timeout;
            _logAction = logAction;
        }

        public string IntegrationDirectory { get; private set; }
        public string ExecutablesDirectory { get; private set; }
        public int Timeout { get; private set; }
        public ExecutableParameters Parameters { get; private set; }

        public void Log(string message)
        {
            if (_logAction == null)
                throw new ArgumentNullException($"LogAction was not set. Cannot call Log on {nameof(ExecutableContext)}.");

            _logAction.Invoke(message);
        }
    }
}
