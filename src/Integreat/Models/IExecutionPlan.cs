﻿using System.Collections.Generic;

namespace Integreat
{
    /// <summary>
    /// List of executable steps and directories for a given plan or integration. Required on every integration process.
    /// </summary>
    public interface IExecutionPlan
    {
        IReadOnlyList<ProcessExecutable> Executables { get; }
        string IntegrationDirectory { get; }
        string ExecutablesDirectory { get; }
    }
}
