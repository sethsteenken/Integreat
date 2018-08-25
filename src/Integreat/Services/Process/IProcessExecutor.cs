using System;

namespace Integreat
{
    public interface IProcessExecutor
    {
        void Execute(Guid id, string processDirectory);
    }
}
