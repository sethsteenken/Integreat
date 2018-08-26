using System;

namespace Integreat
{
    public sealed class Process : IProcess
    {
        private readonly IProcessLogger _logger;
        private readonly IProcessSetup _processSetup;
        private readonly IProcessExecutor _processExecutor;

        public Process(Guid id, IProcessLogger logger, IProcessSetup processSetup, IProcessExecutor processExecutor)
        {
            Guard.IsNotEmptyGuid(id, nameof(id));

            _logger = logger;
            _processSetup = processSetup;
            _processExecutor = processExecutor;
            Id = id;
        }

        public Guid Id { get; private set; }

        public string Execute(string filePath)
        {
            Guard.IsNotNull(filePath, nameof(filePath));

            ProcessWorkingDirectoryContext processDirectoryContext = null;

            try
            {
                _logger.LogInfo("******** Process Started ********");
                _logger.LogInfo($"File Path: {filePath}");
                _logger.LogInfo($"Process ID: {Id}");

                processDirectoryContext = _processSetup.SetupProcessDirectory(Id, filePath);

                _logger.LogInfo("**** Beginning Executables processing. ****");

                _processExecutor.Execute(Id, processDirectoryContext.ProcessDirectory);

                _logger.LogInfo("**** Executables processing completed successfully. ****");
            }
            catch (Exception ex)
            {
                _logger.LogInfo("** FAILURE **");
                _logger.LogInfo($"Process EXCEPTION: {ex}");
                _logger.LogError(ex);
            }
            finally
            {
                try
                {
                    // TODO
                    //_finalizer.OnComplete(success, processDirectoryContext?.WorkingDirectory);
                    _logger.LogInfo("******** Process Complete ********");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex);
                }
            }

            return _logger.ToString();
        }
    }
}
