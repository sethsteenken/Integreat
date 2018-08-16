using Integreat.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace Integreat
{
    public sealed class Process : IProcess
    {
        private readonly ILogger<Process> _logger;
        private readonly StringBuilder _resultBuilder;

        public Process(ILogger<Process> logger)
        {
            _logger = logger;
            Id = Guid.NewGuid();
            _resultBuilder = new StringBuilder();
        }

        public Guid Id { get; private set; }

        public string Execute(ProcessExecutionContext context)
        {
            // TODO - validate the context has executables on the Execution_Plan level

            Guard.IsNotNull(context, nameof(context));

            string workingDirectory = null;

            try
            {
                Log("******** Integration Process Started ********");
                Log($"File Path: {context.FilePath}");

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

                Log("**** Beginning Executables processing. ****");

                foreach (var executableReference in context.Executables)
                {
                    executableReference.Executable.Execute(new ExecutableContext(
                        context.IntegrationDirectory,
                        context.ExecutablesDirectory,
                        executableReference.Parameters,
                        executableReference.Timeout,
                        Log));
                }

                Log("**** Executables processing completed successfully. ****");
            }
            catch (Exception ex)
            {
                Log("** FAILURE **");
                Log($"Process EXCEPTION: {ex}");
                _logger.LogError(ex, ex.Message);
            }
            finally
            {
                try
                {
                    // TODO
                    //_finalizer.OnComplete(success, workingDirectory);
                    Log("******** Integration Process Complete ********");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }

           
            return _resultBuilder.ToString();
        }

        private void Log(string message)
        {
            _logger.LogInformation(message);
            _resultBuilder.AppendLine(LogFormatter.FormatMessage(message));
        }
    }
}
