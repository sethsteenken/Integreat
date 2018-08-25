using System;

namespace Integreat
{
    public interface IProcessSetup
    {
        ProcessWorkingDirectoryContext SetupProcessDirectory(Guid processId, string filePath);
    }
}
