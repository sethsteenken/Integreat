using Integreat.Core;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Text;

namespace Integreat.Powershell
{
    public static class PowershellExtensions
    {
        public static void AddParameters(this PowerShell instance, ExecutableParameters parameters)
        {
            Guard.IsNotNull(instance, nameof(instance));

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    instance.AddParameter(parameter.Name, parameter.Value);
                }
            }
        }

        public static bool Execute(this PowerShell instance, out string result)
        {
            Guard.IsNotNull(instance, nameof(instance));

            var results = instance.Invoke();
            Collection<ErrorRecord> errors = instance.Streams.Error.ReadAll();

            var sb = new StringBuilder();
            sb.AppendLine("Execution complete. Results: ");
            bool success = errors.Count == 0;

            sb.AppendLine(LogFormatter.GetResultCode(success));

            if (results != null)
            {
                foreach (PSObject item in results)
                {
                    sb.AppendLine(item.ToString());
                }
            }

            if (!success)
            {
                foreach (ErrorRecord error in errors)
                {
                    sb.AppendLine(error.ToString());
                }
            }

            result = sb.ToString();
            return success;
        }
    }
}
