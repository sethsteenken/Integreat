using Integreat.Core;
using System;
using System.Diagnostics;
using System.Text;

namespace Integreat
{
    public sealed class BatchExecutable : FileExecutable
    {
        public BatchExecutable(IFileStorage fileStorage, string file) 
            : base(fileStorage, file)
        {
        }

        protected override void OnExecute(ExecutableContext context, IFile file)
        {
            context.Log($"Executing batch file at '{file.FullPath}'...");

            var processInfo = new ProcessStartInfo("cmd.exe")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                Arguments = BuildArguments(file.FullPath, context)
            };

            ExecuteProcess(processInfo, context);
        }

        private void ExecuteProcess(ProcessStartInfo processStartInfo, ExecutableContext context)
        {
            context.Log($"Starting new Windows process...");
            context.Log($"Arguments: {processStartInfo.Arguments}");

            using (var process = System.Diagnostics.Process.Start(processStartInfo))
            {
                process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => context.Log($"Batch file '{File}' OUTPUT => {e.Data}");
                process.BeginOutputReadLine();

                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => context.Log($"Batch file '{File}' ERROR => {e.Data}");
                process.BeginErrorReadLine();

                if (!process.WaitForExit(context.Timeout))
                {
                    try
                    {
                        process.Kill();
                    }
                    catch (Exception ex)
                    {
                        context.Log($"Killing process '{process?.Id}' FAILED. Exception: {ex}");
                    }
                    finally
                    {
                        throw new TimeoutException($"Batch file '{File}' failed to execute in allotted timeout of {context.Timeout} seconds.");
                    }
                }
                else
                {
                    if (process.HasExited)
                        context.Log($"Batch file '{File}' ExitCode: {process.ExitCode}");
                    else
                        context.Log($"Batch file '{File}' NOT EXITED.");

                    process.Close();
                }

                context.Log($"Windows process for batch file '{File}' complete.");
            }
        }

        private string BuildArguments(string filePath, ExecutableContext context)
        {
            string arguments = $"/c \"{filePath}\" \"{context.IntegrationDirectory}\"";

            if (context.Parameters == null || context.Parameters.Count == 0)
                return arguments;

            var sb = new StringBuilder();

            foreach (var parameter in context.Parameters)
            {
                sb.Append(" ");
                sb.Append(parameter.Value);
            }

            return string.Concat(arguments, sb.ToString().TrimEnd());
        }
    }
}
