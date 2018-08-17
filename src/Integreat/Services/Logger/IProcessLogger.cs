using System;

namespace Integreat
{
    public interface IProcessLogger
    {
        void LogInfo(string message);
        void LogError(Exception ex);
    }
}
