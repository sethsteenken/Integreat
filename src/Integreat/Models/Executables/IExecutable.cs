using Integreat.Core;

namespace Integreat
{
    /// <summary>
    /// Execution step that is part of the execution plan.
    /// </summary>
    public interface IExecutable
    {
        void Execute(ExecutableContext context);
    }
}
