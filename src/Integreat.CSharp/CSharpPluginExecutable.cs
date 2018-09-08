using Integreat.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Integreat.CSharp
{
    public class CSharpPluginExecutable : ExecutableBase
    {
        private readonly string _typeName;
        private readonly string _typeAssemblyName;
        private readonly string _pluginExectorPath;

        public CSharpPluginExecutable(string type, string assembly, string pluginExectorPath)
        {
            Guard.IsNotNull(type, nameof(type), $"TypeName value is required to execute {GetType().FullName} executable.");
            Guard.IsNotNull(assembly, nameof(assembly), $"AssemblyName value is required to execute {GetType().FullName} executable.");
            Guard.IsNotNull(pluginExectorPath, nameof(pluginExectorPath));

            _typeName = type.Trim();
            _typeAssemblyName = assembly.Trim();

            if (_typeName.EndsWith(".cs"))
                _typeName = _typeName.Substring(0, _typeName.Length - 3);

            if (_typeAssemblyName.EndsWith(".dll"))
                _typeAssemblyName = _typeAssemblyName.Substring(0, _typeAssemblyName.Length - 4);

            _pluginExectorPath = pluginExectorPath;
        }

        protected string TypeFullName => $"{_typeName}, {_typeAssemblyName}";

        protected override string Name => $"Type: {GetType().Name}, Plugin: {TypeFullName}";

        protected override void OnExecute(ExecutableContext context)
        {
            ExecuteProcess(new ProcessStartInfo(_pluginExectorPath)
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                Arguments = BuildArguments(context)
            }, context);
        }

        private void ExecuteProcess(ProcessStartInfo processStartInfo, ExecutableContext context)
        {
            context.Log("Starting new process for Plugin Executor...");
            context.Log($"Arguments: {processStartInfo.Arguments}");

            using (var process = System.Diagnostics.Process.Start(processStartInfo))
            {
                string output = process.StandardOutput.ReadToEnd();

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
                        throw new TimeoutException($"Plugin Executor process failed to process plugin '{TypeFullName}' in allotted timeout of {context.Timeout} seconds.");
                    }
                }
                else
                {
                    if (process.HasExited)
                        context.Log($"Plugin Executor process finished. ExitCode: {process.ExitCode}");
                    else
                        context.Log($"Plugin Executor process finished. NOT EXITED.");

                    process.Close();
                }

                LogOutput(output, context);

                if (!string.IsNullOrWhiteSpace(output) && output.StartsWith(LogFormatter.FailedResultCode))
                    throw new ApplicationException($"CSharp Plugin '{TypeFullName}' failed execution. See plugin output in logs for details.");
            }

            context.Log("Closed Plugin Executor process.");
        }

        private string BuildArguments(ExecutableContext context)
        {
            var sb = new StringBuilder();

            string libraryPath = Path.Combine(context.ExecutablesDirectory, "plugins", GetAssemblyFileName(_typeAssemblyName));

            sb.AppendKeyValueArgument("integrationdirectory", context.IntegrationDirectory);
            sb.AppendKeyValueArgument("executablesdirectory", context.ExecutablesDirectory);
            sb.AppendKeyValueArgument("library", libraryPath);
            sb.AppendKeyValueArgument("type", _typeName);
            sb.AppendKeyValueArgument("assembly", _typeAssemblyName);

            if (context.Parameters != null)
            {
                foreach (var parameter in context.Parameters)
                {
                    sb.AppendKeyValueArgument(parameter.Key, parameter.Value.ToString());
                }
            }

            return sb.ToString().Trim();
        }

        private static string GetAssemblyFileName(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
                return string.Empty;

            return assemblyName.EndsWith(".dll") ? assemblyName : $"{assemblyName}.dll";
        }

        private void LogOutput(string output, ExecutableContext context)
        {
            context.Log($"Plugin output:");
            string divider = $"----- PLUGIN {_typeName} -----";
            context.Log(divider);
            context.Log(output?.Trim());
            context.Log(new string('-', divider.Length));
        }
    }
}
