using Integreat.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace Integreat
{
    public sealed class Process : IProcess
    {
        private readonly IProcessLogger _logger;

        public Process(IProcessLogger logger)
        {
            _logger = logger;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public string Execute(string filePath)
        {
            // TODO - validate the context has executables on the Execution_Plan level

            Guard.IsNotNull(filePath, nameof(filePath));

            string workingDirectory = null;

            try
            {
                _logger.LogInfo("******** Integration Process Started ********");
                _logger.LogInfo($"File Path: {filePath}");

                // TODO
                //var workingFile = _fileHandler.CopyToWorkingDirectory(filePath);
                //workingDirectory = workingFile.Directory;

                // TODO
                //if (Settings.ArchiveIntegration)
                //    _archiveService.Archive(workingFile.FullPath);

                // TODO
                //string processingDirectory = _fileHandler.GetProcessingDirectory(workingDirectory, workingFile.FullPath);

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
                    _logger.LogInfo("******** Integration Process Complete ********");
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
