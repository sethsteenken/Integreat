using Integreat.Core;
using System;
using System.Management.Automation;

namespace Integreat.Powershell
{
    public sealed class PowershellExecutable : FileExecutable
    {
        public PowershellExecutable(IFileStorage fileStorage, string file) 
            : base(fileStorage, file)
        {
        }

        protected override void OnExecute(ExecutableContext context, IFile file)
        {
            context.Log($"Executing Powershell script at '{file.FullPath}'...");

            using (PowerShell instance = PowerShell.Create())
            {
                instance.AddCommand(file.FullPath);
                instance.AddParameters(context.Parameters);

                context.Log($"Parameter Arguments: {context.Parameters}");

                var success = instance.Execute(out string result);

                context.Log(result);

                if (!success)
                    throw new ApplicationException($"PowerShell script '{File}' failed execution. See output in logs for details.");
            }
        }
    }
}
