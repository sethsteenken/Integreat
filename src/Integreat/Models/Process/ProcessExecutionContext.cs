﻿using System.Collections.Generic;

namespace Integreat
{
    public class ProcessExecutionContext
    {
        public IReadOnlyList<ProcessExecutable> Executables { get; private set; }
        public string IntegrationDirectory { get; private set; }
        public string ExecutablesDirectory { get; private set; }
    }
}
