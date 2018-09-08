using Microsoft.Extensions.DependencyInjection;
using System;

namespace Integreat
{
    public static class IntegreatServicesBuilderExtensions
    {
        public static IIntegreatServicesBuilder AddExecutableAdapter(this IIntegreatServicesBuilder builder, Func<IServiceProvider, IProcessExecutableAdapter> adapterFactory)
        {
            return AddExecutableAdapter(builder, adapterFactory, ServiceLifetime.Singleton);
        }

        public static IIntegreatServicesBuilder AddExecutableAdapter(this IIntegreatServicesBuilder builder, Func<IServiceProvider, IProcessExecutableAdapter> adapterFactory, ServiceLifetime lifetime)
        {
            Guard.IsNotNull(builder, nameof(builder));

            builder.Services.Add(new ServiceDescriptor(typeof(IProcessExecutableAdapter), adapterFactory, lifetime));
            return builder;
        }

        public static IIntegreatServicesBuilder AddExecutableAdapter<T>(this IIntegreatServicesBuilder builder) where T : class, IProcessExecutableAdapter 
        {
            return AddExecutableAdapter<T>(builder, ServiceLifetime.Singleton);
        }

        public static IIntegreatServicesBuilder AddExecutableAdapter<T>(this IIntegreatServicesBuilder builder, ServiceLifetime lifetime) where T : class, IProcessExecutableAdapter
        {
            Guard.IsNotNull(builder, nameof(builder));

            builder.Services.Add(new ServiceDescriptor(typeof(IProcessExecutableAdapter), typeof(T), lifetime));
            return builder;
        }

        public static IIntegreatServicesBuilder AddSettings(this IIntegreatServicesBuilder builder, IIntegrationSettings settings)
        {
            Guard.IsNotNull(builder, nameof(builder));
            Guard.IsNotNull(settings, nameof(settings));

            builder.Services.AddSingleton<IIntegrationSettings>(settings);

            return builder;
        }

        public static IIntegreatServicesBuilder AddSettings(this IIntegreatServicesBuilder builder, Func<IServiceProvider, IIntegrationSettings> settingsFactory)
        {
            return AddSettings(builder, settingsFactory, ServiceLifetime.Singleton);

        }

        public static IIntegreatServicesBuilder AddSettings(this IIntegreatServicesBuilder builder, Func<IServiceProvider, IIntegrationSettings> settingsFactory, ServiceLifetime lifetime)
        {
            Guard.IsNotNull(builder, nameof(builder));
            Guard.IsNotNull(settingsFactory, nameof(settingsFactory));

            builder.Services.Add(new ServiceDescriptor(typeof(IIntegrationSettings), settingsFactory, lifetime));

            return builder;
        }
    }
}
