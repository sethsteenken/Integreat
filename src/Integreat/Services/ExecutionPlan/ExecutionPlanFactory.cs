using System;
using System.Collections.Generic;
using System.Text;

namespace Integreat
{
    public class ExecutionPlanFactory : IExecutionPlanFactory
    {
        private readonly IProcessLogger _logger;

        public ExecutionPlanFactory(IProcessLogger logger)
        {
            _logger = logger;
        }

        public IExecutionPlan Create(string processDirectory)
        {
            throw new NotImplementedException();
        }
    }
}
