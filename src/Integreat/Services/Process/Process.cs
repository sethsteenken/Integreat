using Integreat.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace Integreat
{
    public sealed class Process : IProcess
    {
        private readonly IProcessLogger _logger;
        private readonly IIntegrationFileHandler _fileHandler;
        private readonly IIntegrationArchiver _archiver;

        // TODO - remove after class is completed
        internal Process() { }

        public Process(Guid id, IProcessLogger logger, IIntegrationFileHandler fileHandler, IIntegrationArchiver archiver)
        {
            Guard.IsNotEmptyGuid(id, nameof(id));

            _logger = logger;
            _fileHandler = fileHandler;
            _archiver = archiver;
            Id = id;
        }

        public Guid Id { get; private set; }

        public string Execute(string filePath)
        {
            // TODO - validate the context has executables on the Execution_Plan level

            Guard.IsNotNull(filePath, nameof(filePath));

            string workingDirectory = null;

            try
            {
                _logger.LogInfo("******** Process Started ********");
                _logger.LogInfo($"File Path: {filePath}");
                _logger.LogInfo($"Process ID: {Id}");

                var workingFile = _fileHandler.CopyToWorkingDirectory(filePath, Id);
                workingDirectory = workingFile.Directory;

                _archiver.Archive(workingFile.FullPath, Id);

                string processingDirectory = _fileHandler.GetProcessingDirectory(workingDirectory, workingFile.FullPath);

                // TODO
                //var executionPlan = _executionPlanHandler.ConstructExecutionPlan(processingDirectory);

                _logger.LogInfo("**** Beginning Executables processing. ****");

                // TODO
                //foreach (var executableReference in context.Executables)
                //{
                //    executableReference.Executable.Execute(new ExecutableContext(
                //        context.IntegrationDirectory,
                //        context.ExecutablesDirectory,
                //        executableReference.Parameters,
                //        executableReference.Timeout,
                //        _logger.LogInfo));
                //}

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
                    //_finalizer.OnComplete(success, workingDirectory);
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
