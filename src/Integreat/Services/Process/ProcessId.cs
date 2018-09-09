using System;

namespace Integreat
{
    public sealed class ProcessId
    {
        public ProcessId()
        {
            CurrentGuid = Guid.NewGuid();
        }

        public Guid CurrentGuid { get; private set; }
    }
}
