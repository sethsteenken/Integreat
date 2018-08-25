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

            // TODO - change this up per the builder changes below - these are temporary to show concept of what needs to happen
            IDictionary<string, ExecutableConfiguration> configurations = new Dictionary<string, ExecutableConfiguration>();
            IDictionary<string, IExecutable> executableRegistrations = new Dictionary<string, IExecutable>();

            var processExecutables = new List<ProcessExecutable>();

            foreach (dynamic executableReference in reference.Executables)
            {
                Type referenceType = executableReference.GetType();
                if (!referenceType.GetProperties().Any(p => p.Name.Equals("Type")))
                    throw new InvalidOperationException("An executable reference does not contain required Type property.");

                string executableType = executableReference.Type as string;

                // TODO - change this to some sort of builder that will take the string Type AND the dynamic object in order to build out ExecutableConfiguration
                //      - a builder may be needed for each ExecutableConfiguration type, and then register each builder
                if (!configurations.TryGetValue(executableType, out ExecutableConfiguration config))
                    throw new InvalidOperationException($"Executable of type '{executableType}' was not found in available configurations.");

                // TODO - change this to some sort of builder because the IExecutable relies on the provided ExecutableConfiguration
                if (!executableRegistrations.TryGetValue(executableType, out IExecutable executable))
                    throw new InvalidOperationException($"Executable of type '{executableType}' was not found in registered executable types.");

                processExecutables.Add(new ProcessExecutable(executable, config));
            }

            return new ExecutionPlan(
                processExecutables,
                integrationDirectory: processDirectory,
                executablesDirectory: file.Directory);
        }

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
