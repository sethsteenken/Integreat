using System;
using System.IO;
using System.Linq;

namespace Integreat
{
    public class IntegrationFileHandler : IIntegrationFileHandler
    {
        private readonly IFileStorage _fileStorage;
        private readonly IProcessLogger _logger;
        private readonly IIntegrationSettings _settings;

        public IntegrationFileHandler(IFileStorage fileStorage, IProcessLogger logger, IIntegrationSettings settings)
        {
            _fileStorage = fileStorage;
            _logger = logger;
            _settings = settings;
        }

        public IFile CopyToWorkingDirectory(Guid processId, string filePath)
        {
            if (!_fileStorage.Exists(filePath))
                throw new FileNotFoundException($"File at path '{filePath}' not found.");

            _logger.LogInfo("Waiting on file to be ready...");
            _fileStorage.WaitForFileReady(filePath, _settings.DropFileReadyTimeout);
            _logger.LogInfo("File ready.");

            var file = _fileStorage.GetFile(filePath);
            ValidateIntegrationFile(file);

            var workingFileInfo = GetWorkingFileInfo(file, processId);
            _logger.LogInfo($"Copying dropped file at '{file}' to '{workingFileInfo}'...");
            string workingFilePath = _fileStorage.CopyFile(file.FullPath, workingFileInfo.FullPath);
            _logger.LogInfo("Copying complete.");

            if (_settings.DeleteFromDropDirectory)
            {
                _logger.LogInfo($"Deleting dropped file at '{filePath}'...");
                _fileStorage.Delete(filePath);
                _logger.LogInfo($"Dropped file deleted.");
            }

            return _fileStorage.GetFile(workingFilePath);
        }

        private void ValidateIntegrationFile(IFile file)
        {
            if (file == null || string.IsNullOrWhiteSpace(file.FullPath))
                throw new NullReferenceException("Integration file path empty or null.");

            if (string.IsNullOrWhiteSpace(file.Extension))
                throw new InvalidOperationException($"Integration file '{file.FullPath}' does not have an extension.");
        }

        private IFile GetWorkingFileInfo(IFile droppedFile, Guid processId)
        {
            string directory = Path.Combine(
                _settings.ServiceWorkingDirectory,
                processId.ToString().Replace("-", "_"));

            _fileStorage.CreateDirectory(directory);

            return _fileStorage.GetFile(Path.Combine(directory, droppedFile.FileName));
        }

        public string GetProcessingDirectory(IFile workingFile)
        {
            Guard.IsNotNull(workingFile, nameof(workingFile));

            // unzip if .zip file, else just use working directory assuming static file was dropped
            if (NeedsExtraction(workingFile))
                return Unzip(workingFile);
            else
                return workingFile.Directory;
        }

        private bool NeedsExtraction(IFile file)
        {
            return file.Extension == "zip";
        }

        private string Unzip(IFile file)
        {
            if (!NeedsExtraction(file))
                throw new InvalidOperationException($"Integration file '{file}' must be in .zip format if attempting to extract.");

            var outputDirectory = Path.Combine(file.Directory, "__output");
            _fileStorage.CreateDirectory(outputDirectory);

            _logger.LogInfo($"Extracting integration files from '{file}' to directory '{outputDirectory}'...");

            _fileStorage.Extract(file.FullPath, outputDirectory);
            _logger.LogInfo("Extracting complete.");
            return outputDirectory;
        }

        public IFile FindExecutionPlanFile(string directory)
        {
            IFile file = null;
            bool allowPlanWithIntegration = _settings.AllowExecutionPlanWithIntegration;
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (allowPlanWithIntegration)
                file = _fileStorage.GetFiles(directory, _settings.ExecutionPlanFileName).FirstOrDefault();

            // if not provided in integration, attempt to find it in root of this service application
            if (file == null)
                file = _fileStorage.GetFiles(appDirectory, _settings.ExecutionPlanFileName).FirstOrDefault();

            if (file == null)
            {
                string message = $"Execution Plan configuration file '{_settings.ExecutionPlanFileName}' not found.";

                if (allowPlanWithIntegration)
                    message = string.Concat(message, " ", $"Searched integration directory and sub-directories at '{directory}'.");

                message = string.Concat(message, " ", $"Searched for default execution plan file in application directory and sub-directories at '{appDirectory}'.");

                throw new FileNotFoundException(message);
            }

            return file;
        }
    }
}
