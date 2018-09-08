using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integreat.CSharp
{
    public static class IntegreatServicesBuilderExtensions
    {
        public static IIntegreatServicesBuilder AddCSharpPluginExecutable(this IIntegreatServicesBuilder builder)
        {
            return builder.AddExecutableAdapter((serviceProvider) => new CSharpPluginProcessExecutableAdapter(serviceProvider.GetRequiredService<IIntegrationSettings>().PluginExecutorAppPath));
        }
    }
}
