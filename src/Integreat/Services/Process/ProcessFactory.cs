using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Integreat
{
    public class ProcessFactory : IProcessFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IFileStorage _fileStorage;
        private readonly IIntegrationSettings _settings;
        private readonly ISerializer _serializer;
        private readonly IDictionary<string, IProcessExecutableAdapter> _adapters;

        public ProcessFactory(ILoggerFactory loggerFactory, 
            IFileStorage fileStorage, 
            IIntegrationSettings settings, 
            ISerializer serializer, 
            IDictionary<string, IProcessExecutableAdapter> adapters)
        {
            _loggerFactory = loggerFactory;
            _fileStorage = fileStorage;
            _settings = settings;
            _serializer = serializer;
            _adapters = adapters;
        }

        public IProcess Create()
        {
            var processId = Guid.NewGuid();

            IProcessLogger processLogger = new ProcessLogger(_loggerFactory.CreateLogger<ProcessLogger>(), processId);

            IIntegrationArchiver archiver = new IntegrationArchiver(_fileStorage, processLogger, _settings);
            IIntegrationFileHandler fileHandler = new IntegrationFileHandler(_fileStorage, processLogger, _settings);
            IProcessSetup processSetup = new ProcessSetup(archiver, fileHandler);
            IExecutionPlanFactory executionPlanFactory = new ExecutionPlanFactory(_serializer, fileHandler, _adapters);
            IProcessExecutor processExecutor = new ProcessExecutor(processLogger, executionPlanFactory);

            return new Process(processId, processLogger, processSetup, processExecutor);
        }
    }
}
