using System;

namespace Integreat
{
    public interface IWatcher : IDisposable
    {
        void Initialize();
    }
}
