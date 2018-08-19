using System;

namespace Integreat
{
    public interface IIntegrationFileHandler
    {
        IFile CopyToWorkingDirectory(string filePath, Guid processId);
        string GetProcessingDirectory(string workingDirectory, string workingFilePath);
    }
}
