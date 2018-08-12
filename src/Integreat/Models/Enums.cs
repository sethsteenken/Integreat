using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Integreat
{
    /// <summary>
    /// Available types of execution languages/processes.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ExecutableTypeOption
    {
        Undefined = 1,
        CSharpPlugin,
        Powershell,
        Batch,
        SQL
    }
}
