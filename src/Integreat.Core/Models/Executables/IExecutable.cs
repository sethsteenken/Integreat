using Integreat.Plugins;

namespace Integreat.Core
{
    /// <summary>
    /// Execution step that is part of the execution plan.
    /// </summary>
    public interface IExecutable
    {
        string Name { get; }
        void Execute(ExecutableContext context);
    }
}
