namespace Integreat.CSharp
{
    public static class IntegreatServicesBuilderExtensions
    {
        public static IIntegreatServicesBuilder AddCSharpPluginExecutable(this IIntegreatServicesBuilder builder)
        {
            return AddCSharpPluginExecutable(builder, "Integreat.PluginExecutor.dll");
        }

        public static IIntegreatServicesBuilder AddCSharpPluginExecutable(this IIntegreatServicesBuilder builder, string pluginExecutorAppPath)
        {
            Guard.IsNotNull(builder, nameof(builder));
            Guard.IsNotNull(pluginExecutorAppPath, nameof(pluginExecutorAppPath));

            return builder.AddExecutableAdapter((serviceProvider) => new CSharpPluginProcessExecutableAdapter(pluginExecutorAppPath));
        }
    }
}
