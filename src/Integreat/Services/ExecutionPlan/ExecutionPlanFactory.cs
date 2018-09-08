using System;
using System.Collections.Generic;
using System.Linq;

namespace Integreat
{
    public class ExecutionPlanFactory : IExecutionPlanFactory
    {
        private readonly ISerializer _serializer;
        private readonly IIntegrationFileHandler _fileHandler;
        private readonly IDictionary<string, IProcessExecutableAdapter> _adapters;

        public ExecutionPlanFactory(
            ISerializer serializer, 
            IIntegrationFileHandler fileHandler, 
            IDictionary<string, IProcessExecutableAdapter> adapters)
        {
            _serializer = serializer;
            _fileHandler = fileHandler;
            _adapters = adapters;
        }

        public IExecutionPlan Create(string processDirectory)
        {
            Guard.IsNotNull(processDirectory, nameof(processDirectory));

            var executionPlanFile = _fileHandler.FindExecutionPlanFile(processDirectory);
            dynamic reference = _serializer.DeserializeFile<dynamic>(executionPlanFile.FullPath);

            if (reference == null)
                throw new InvalidOperationException("Deserialized result of Execution Plan is null or invalid.");

            return new ExecutionPlan(
                BuildProcessExecutables(reference),
                integrationDirectory: processDirectory,
                executablesDirectory: executionPlanFile.Directory);
        }

        private IReadOnlyList<ProcessExecutable> BuildProcessExecutables(dynamic reference)
        {
            Type type = reference.GetType();
            if (!type.GetProperties().Any(p => p.Name.Equals("Executables")))
                throw new InvalidOperationException("Execution plan does not contain Executables.");

            var processExecutables = new List<ProcessExecutable>();

            foreach (dynamic executableReference in reference.Executables)
            {
                Type referenceType = executableReference.GetType();
                if (!referenceType?.GetProperties()?.Exists("Type") ?? false)
                    throw new InvalidOperationException("An executable reference does not contain required 'Type' property.");

                string executableType = executableReference.Type as string;

                if (!_adapters.TryGetValue(executableType, out IProcessExecutableAdapter adapter))
                    throw new InvalidOperationException($"Executable of type '{executableType}' does not have a registered adapter to build the configuration and executable.");

                processExecutables.Add(adapter.Build(executableReference));
            }

            return processExecutables;
        }
    }
}
