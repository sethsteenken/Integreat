using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Integreat
{
    public static class ServiceCollectionExtensions
    {
        public static IIntegreatServicesBuilder AddIntegreat(this IServiceCollection services)
        {
            return AddIntegreat(services, null);
        }

        public static IIntegreatServicesBuilder AddIntegreat(this IServiceCollection services, string dropDirectory)
        {
            Guard.IsNotNull(services, nameof(services));

            if (!string.IsNullOrWhiteSpace(dropDirectory))
                services.AddSingleton<IIntegrationSettings>(new IntegrationSettings() { DropDirectory = dropDirectory });

            if (!services.Any(s => s.ServiceType == typeof(ILoggerFactory)))
            {
                services
                    .AddSingleton<ILoggerFactory, ConsoleLoggerFactory>()
                    .AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            }

            services.AddSingleton<IFileStorage, SystemIOFileStorage>();
            services.AddSingleton<ISerializer, JsonNetSerializer>();
            services.AddSingleton<IProcessFactory, ProcessFactory>();
            services.AddSingleton<IWatcher, FileWatcher>();

            services.AddScoped<IDictionary<string, IProcessExecutableAdapter>>((serviceProvider) =>
            {
                return serviceProvider.GetServices<IProcessExecutableAdapter>()?.ToDictionary((adapter) => adapter.Type)
                            ?? new Dictionary<string, IProcessExecutableAdapter>();
            });

            services.AddScoped<ProcessId>()
                .AddScoped<IProcessLogger, ProcessLogger>()
                .AddScoped<IIntegrationArchiver, IntegrationArchiver>()
                .AddScoped<IIntegrationFileHandler, IntegrationFileHandler>()
                .AddScoped<IExecutionPlanFactory, ExecutionPlanFactory>()
                .AddScoped<IProcessExecutor, ProcessExecutor>()
                .AddScoped<IProcessSetup, ProcessSetup>()
                .AddScoped<IProcess, Process>();

            return new IntegreatServicesBuilder(services);
        }
    }
}
