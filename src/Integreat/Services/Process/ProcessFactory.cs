using Microsoft.Extensions.Logging;
using System;

namespace Integreat
{
    public class ProcessFactory : IProcessFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IProcessSetup _processSetup;
        private readonly IProcessExecutor _processExecutor;

        public ProcessFactory(ILoggerFactory loggerFactory, IProcessSetup processSetup, IProcessExecutor processExecutor)
        {
            _loggerFactory = loggerFactory;
            _processSetup = processSetup;
            _processExecutor = processExecutor;
        }

        public IProcess Create()
        {
            var processId = Guid.NewGuid();
            IProcessLogger processLogger = new ProcessLogger(_loggerFactory.CreateLogger<ProcessLogger>(), processId);

            return new Process(processId, processLogger, _processSetup, _processExecutor);
        }
    }
}
