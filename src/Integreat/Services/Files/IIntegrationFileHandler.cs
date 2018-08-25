using System;

namespace Integreat
{
    public interface IIntegrationFileHandler
    {
        IFile CopyToWorkingDirectory(Guid processId, string filePath);
        string GetProcessingDirectory(IFile workingFile);
    }
}
