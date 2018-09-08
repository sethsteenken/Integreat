using System;

namespace Integreat
{
    internal class ProcessId
    {
        public ProcessId()
        {
            CurrentGuid = Guid.NewGuid();
        }

        public Guid CurrentGuid { get; private set; }
    }
}
