using System;

namespace Integreat
{
    public interface IProcess
    {
        Guid Id { get; }
        string Execute(ProcessExecutionContext context);
    }
}
