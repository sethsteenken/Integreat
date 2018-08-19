using System;
using System.IO;

namespace Integreat
{
    public class IntegrationArchiver : IIntegrationArchiver
    {
        private readonly IFileStorage _fileStorage;
        private readonly IProcessLogger _logger;
        private readonly IIntegrationSettings _settings;

        public IntegrationArchiver(IFileStorage fileStorage, IProcessLogger logger, IIntegrationSettings settings)
        {
            _fileStorage = fileStorage;
            _logger = logger;
            _settings = settings;
        }

        public void Archive(string filePath, Guid processId)
        {
            if (!_settings.ArchiveIntegration)
                return;

            _logger.LogInfo("Archiving integration...");
            var archiveFile = GetArchiveFile(filePath, processId);

            _logger.LogInfo($"Copying '{filePath}' to '{archiveFile.FullPath}'...");
            _fileStorage.CopyFile(filePath, archiveFile.FullPath);
            _logger.LogInfo("Copying complete.");
            _logger.LogInfo($"Integration files archived to '{archiveFile.FullPath}'.");

            CleanUpArchive(archiveFile.Directory, _settings.ArchiveLimit);
        }

        private IFile GetArchiveFile(string filePath, Guid processId)
        {
            var file = _fileStorage.GetFile(filePath);
            string archiveFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.Now.ToString("yyyy_MM_dd")}_{processId}.{file.Extension}";
            return _fileStorage.GetFile(Path.Combine(_settings.ArchiveDirectory, archiveFileName));
        }

        private void CleanUpArchive(string archiveDirectory, int limit)
        {
            var archiveFiles = _fileStorage.GetFiles(archiveDirectory);

            if (archiveFiles == null || archiveFiles.Count == 0)
                return;

            _logger.LogInfo($"Cleaning up any archive files in '{archiveDirectory}' (keeping {limit} in archive)...");

            for (int i = 0; i < archiveFiles.Count; i++)
            {
                if ((i + 1) > limit)
                {
                    string fullPath = archiveFiles[i].FullPath;
                    _fileStorage.Delete(fullPath);
                    _logger.LogInfo($"Archive '{fullPath}' deleted.");
                }
            }

            _logger.LogInfo("Archive cleanup complete.");
        }
    }
}
