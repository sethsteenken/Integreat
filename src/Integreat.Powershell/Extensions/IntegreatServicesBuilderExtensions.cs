namespace Integreat.Powershell
{
    public static class IntegreatServicesBuilderExtensions
    {
        public static IIntegreatServicesBuilder AddPowershellExecutable(this IIntegreatServicesBuilder builder)
        {
            return builder.AddExecutableAdapter<PowershellProcessExecutableAdapter>();
        }
    }
}
