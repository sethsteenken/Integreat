using System;

namespace Integreat
{
    public class ProcessSetup : IProcessSetup
    {
        private readonly IIntegrationArchiver _archiver;
        private readonly IIntegrationFileHandler _fileHandler;

        public ProcessSetup(IIntegrationArchiver archiver, IIntegrationFileHandler fileHandler)
        {
            _archiver = archiver;
            _fileHandler = fileHandler;
        }

        public ProcessWorkingDirectoryContext SetupProcessDirectory(Guid processId, string filePath)
        {
            var workingFile = _fileHandler.CopyToWorkingDirectory(processId, filePath);

            _archiver.Archive(processId, workingFile.FullPath);

            string processingDirectory = _fileHandler.GetProcessingDirectory(workingFile);

            return new ProcessWorkingDirectoryContext(workingFile.Directory, processingDirectory);
        }
    }
}
