using System;

namespace Integreat
{
    public interface IProcess : IDisposable
    {
        string Execute(string filePath);
    }
}
