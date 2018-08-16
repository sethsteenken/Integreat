using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Integreat
{
    public sealed class FileWatcher : IWatcher
    {
        private const int _maxBufferSize = 65536;

        private readonly IIntegrationSettings _settings;
        private readonly IProcessFactory _processFactory;
        private readonly ILogger<FileWatcher> _logger;

        private FileSystemWatcher _watcher;
        private bool _disposed = false;

        public FileWatcher(IIntegrationSettings settings,
            IProcessFactory processFactory,
            ILogger<FileWatcher> logger)
        {
            _settings = settings;
            _processFactory = processFactory;
            _logger = logger;
        }

        public void Initialize()
        {
            if (_watcher != null)
            {
                _watcher?.Dispose();
                _watcher = null;
            }

            _watcher = new FileSystemWatcher(
                _settings.DropDirectory,
                _settings.DropFileName)
            {
                NotifyFilter = NotifyFilters.LastWrite,
                InternalBufferSize = _maxBufferSize
            };

            _watcher.Changed += new FileSystemEventHandler(OnIntegrationFilesDropped);
            _watcher.Error += new ErrorEventHandler(OnError);

            _watcher.EnableRaisingEvents = true;
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            var ex = e.GetException();
            _logger.LogError(ex, ex.Message);
        }

        private void OnIntegrationFilesDropped(object sender, FileSystemEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.FullPath))
                return;

            try
            {
                // help prevent duplicate event calls
                _watcher.EnableRaisingEvents = false;

                IProcess process = _processFactory.Create();
                process.Execute(new ProcessExecutionContext() { FilePath = e.FullPath });

                _watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _watcher?.Dispose();
                _watcher = null;
                _disposed = true;
            }
        }
    }
}
