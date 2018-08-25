using Integreat.Core;

namespace Integreat
{
    public sealed class ProcessExecutable
    {
        public IExecutable Executable { get; private set; }
        public int Timeout { get; private set; }
        public ExecutableParameters Parameters { get; private set; }
    }
}
