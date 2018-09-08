using Integreat.Core;
using System;
using System.Collections.Generic;

namespace Integreat.PluginExecutor
{
    internal class ExecutorSettings
    {
        private const int _requiredArgumentCount = 5;

        public ExecutorSettings(string[] arguments)
        {
            SetValuesFromArguments(arguments);
            Validate();
        }

        public string IntegrationDirectory { get; private set; }
        public string ExecutablesDirectory { get; private set; }
        public string LibraryPath { get; private set; }
        public string TypeName { get; private set; }
        public string AssemblyName { get; private set; }
        public ExecutableParameters Parameters { get; private set; }

        private void SetValuesFromArguments(string[] arguments)
        {
            if (arguments == null || arguments.Length == 0)
                throw new ArgumentNullException(nameof(arguments));

            if (arguments.Length < _requiredArgumentCount)
                throw new InvalidOperationException($"Not all required arguments ({_requiredArgumentCount}) were supplied. Argument count: {arguments.Length}.");

            IDictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var arg in arguments)
            {
                var keyValue = arg.Split('=');
                if (keyValue.Length != 2)
                    throw new InvalidOperationException($"Argument '{arg}' not in correct format of 'key=value'.");

                string key = keyValue[0]?.Trim();
                string value = keyValue[1]?.Trim();

                if (string.IsNullOrWhiteSpace(key))
                    throw new ArgumentNullException("Argument key empty or null.");

                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Argument value empty or null.");

                if (key == ParameterKeys.IntegrationDirectory && string.IsNullOrWhiteSpace(IntegrationDirectory))
                    IntegrationDirectory = value;
                else if (key == ParameterKeys.ExecutablesDirectory && string.IsNullOrWhiteSpace(ExecutablesDirectory))
                    ExecutablesDirectory = value;
                else if (key == ParameterKeys.Library && string.IsNullOrWhiteSpace(LibraryPath))
                    LibraryPath = value.EndsWith(".dll") ? value : $"{value}.dll";
                else if (key == ParameterKeys.Type && string.IsNullOrWhiteSpace(TypeName))
                    TypeName = value;
                else if (key == ParameterKeys.Assembly && string.IsNullOrWhiteSpace(AssemblyName))
                    AssemblyName = value;
                else
                    parameters.Add(key, value);
            }

            Parameters = new ExecutableParameters(parameters);
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(IntegrationDirectory))
                throw new ArgumentNullException($"Required argument with key of '{ParameterKeys.IntegrationDirectory}' was not supplied.");

            if (string.IsNullOrWhiteSpace(ExecutablesDirectory))
                throw new ArgumentNullException($"Required argument with key of '{ParameterKeys.ExecutablesDirectory}' was not supplied.");

            if (string.IsNullOrWhiteSpace(LibraryPath))
                throw new ArgumentNullException($"Required argument with key of '{ParameterKeys.Library}' was not supplied.");

            if (string.IsNullOrWhiteSpace(TypeName))
                throw new ArgumentNullException($"Required argument with key of '{ParameterKeys.Type}' was not supplied.");

            if (string.IsNullOrWhiteSpace(AssemblyName))
                throw new ArgumentNullException($"Required argument with key of '{ParameterKeys.Assembly}' was not supplied.");
        }
    }
}
