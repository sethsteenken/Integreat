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
            builder.Services.Add(new ServiceDescriptor(typeof(IProcessExecutableAdapter), adapterFactory, lifetime));
            return builder;
        }

        public static IIntegreatServicesBuilder AddExecutableAdapter<T>(this IIntegreatServicesBuilder builder) where T : class, IProcessExecutableAdapter 
        {
            return AddExecutableAdapter<T>(builder, ServiceLifetime.Singleton);
        }

        public static IIntegreatServicesBuilder AddExecutableAdapter<T>(this IIntegreatServicesBuilder builder, ServiceLifetime lifetime) where T : class, IProcessExecutableAdapter
        {
            builder.Services.Add(new ServiceDescriptor(typeof(IProcessExecutableAdapter), typeof(T), lifetime));
            return builder;
        }

        public static IIntegreatServicesBuilder AddCSharpPluginExecutable(this IIntegreatServicesBuilder builder)
        {
            return builder.AddExecutableAdapter((serviceProvider) => new CSharpPluginProcessExecutableAdapter(serviceProvider.GetRequiredService<IIntegrationSettings>().PluginExecutorAppPath));
        }

        public static IIntegreatServicesBuilder AddBatchExecutable(this IIntegreatServicesBuilder builder)
        {
            return builder.AddExecutableAdapter<BatchProcessExecutableAdapter>();
        }
    }
}
