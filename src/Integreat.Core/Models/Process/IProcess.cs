using System;

namespace Integreat.Core
{
    public interface IProcess
    {
        Guid Id { get; }
        string Result { get; }
        void Execute();
    }
}
