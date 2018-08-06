using System;

namespace Integreat
{
    public interface IProcess
    {
        Guid Id { get; }
        string Result { get; }
        void Execute();
    }
}
