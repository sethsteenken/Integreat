﻿using System.Collections.Generic;

namespace Integreat.Core
{
    public sealed class ExecutionPlan : IExecutionPlan
    {
        private ExecutionPlan() { }

        public IReadOnlyList<IExecutable> Executables { get; private set; }
        public string IntegrationDirectory { get; private set; }
        public string ExecutablesDirectory { get; private set; }
    }
}
