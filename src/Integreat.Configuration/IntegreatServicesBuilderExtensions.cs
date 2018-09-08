using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO;

namespace Integreat.Configuration
{
    public static class IntegreatServicesBuilderExtensions
    {
        public static IIntegreatServicesBuilder AddJsonConfigurationSettings(this IIntegreatServicesBuilder builder)
        {
            return AddJsonConfigurationSettings(builder, asSingleton: true);
        }

        public static IIntegreatServicesBuilder AddJsonConfigurationSettings(this IIntegreatServicesBuilder builder, bool asSingleton)
        {
            Guard.IsNotNull(builder, nameof(builder));

            builder.Services.TryAddSingleton<IConfiguration>(new ConfigurationBuilder()
                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: !asSingleton)
                                                .Build());

            builder.Services.AddCustomConfigurationSettings<IIntegrationSettings, IntegrationSettings>("Integreat", asSingleton);

            return builder;
        }
    }
}
