﻿using Integreat.Core;
using System;

namespace Integreat
{
    public class ProcessExecutor : IProcessExecutor
    {
        private readonly IProcessLogger _logger;
        private readonly IExecutionPlanFactory _executionPlanFactory;

        public ProcessExecutor(IProcessLogger logger, IExecutionPlanFactory executionPlanFactory)
        {
            _logger = logger;
            _executionPlanFactory = executionPlanFactory;
        }

        public void Execute(Guid id, string processDirectory)
        {         
            var executionPlan = _executionPlanFactory.Create(processDirectory);

            foreach (var executableReference in executionPlan.Executables)
            {
                executableReference.Executable.Execute(new ExecutableContext(
                    executionPlan.IntegrationDirectory,
                    executionPlan.ExecutablesDirectory,
                    executableReference.Parameters,
                    executableReference.Timeout,
                    _logger.LogInfo));
            }
        }
    }
}