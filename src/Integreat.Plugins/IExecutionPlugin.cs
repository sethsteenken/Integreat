using Integreat.Core;

namespace Integreat.Plugins
{
    public interface IExecutionPlugin
    {
        void Execute(ExecutableContext context);
    }
}
