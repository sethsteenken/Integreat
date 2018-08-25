using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Integreat
{
    public class ExecutionPlanFactory : IExecutionPlanFactory
    {
        private readonly IProcessLogger _logger;
        private readonly ISerializer _serializer;
        private readonly IFileStorage _fileStorage;
        private readonly IIntegrationSettings _settings;

        public ExecutionPlanFactory(IProcessLogger logger, IIntegrationSettings settings, ISerializer serializer, IFileStorage fileStorage)
        {
            _logger = logger;
            _serializer = serializer;
            _fileStorage = fileStorage;
            _settings = settings;
        }

        public IExecutionPlan Create(string processDirectory)
        {
            Guard.IsNotNull(processDirectory, nameof(processDirectory));

            var file = GetConfigFile(processDirectory);
            dynamic reference = _serializer.DeserializeFile<dynamic>(file.FullPath);

            if (reference == null)
                throw new InvalidOperationException("Deserialized result of Execution Plan is null or invalid");

            Type type = reference.GetType();
            if (!type.GetProperties().Any(p => p.Name.Equals("Executables")))
                throw new InvalidOperationException("Execution plan does not contain Executables.");

            // TODO - need these registered/injected somewhere - type "string" lookup => adapter
            IDictionary<string, IProcessExecutableAdapter> adapters = new Dictionary<string, IProcessExecutableAdapter>();

            var processExecutables = new List<ProcessExecutable>();

            foreach (dynamic executableReference in reference.Executables)
            {
                Type referenceType = executableReference.GetType();
                if (!referenceType.GetProperties().Any(p => p.Name.Equals("Type")))
                    throw new InvalidOperationException("An executable reference does not contain required Type property.");

                string executableType = executableReference.Type as string;

                if (!adapters.TryGetValue(executableType, out IProcessExecutableAdapter adapter))
                    throw new InvalidOperationException($"Executable of type '{executableType}' does not have a registered adapter to build the configuration and executable.");

                processExecutables.Add(adapter.Build(executableReference));
            }

            return new ExecutionPlan(
                processExecutables,
                integrationDirectory: processDirectory,
                executablesDirectory: file.Directory);
        }

        // TODO - move functionality to another class
        private IFile GetConfigFile(string directory)
        {
            IFile file = null;
            bool allowPlanWithIntegration = _settings.AllowExecutionPlanWithIntegration;
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (allowPlanWithIntegration)
                file = _fileStorage.GetFiles(directory, _settings.ExecutionPlanFileName).FirstOrDefault();

            // if not provided in integration, attempt to find it in root of this service application
            if (file == null)
                file = _fileStorage.GetFiles(appDirectory, _settings.ExecutionPlanFileName).FirstOrDefault();

            if (file == null)
            {
                string message = $"Execution Plan configuration file '{_settings.ExecutionPlanFileName}' not found.";

                if (allowPlanWithIntegration)
                    message = string.Concat(message, " ", $"Searched integration directory and sub-directories at '{directory}'.");

                message = string.Concat(message, " ", $"Searched for default execution plan file in application directory and sub-directories at '{appDirectory}'.");

                throw new FileNotFoundException(message);
            }

            return file;
        }
    }
}
