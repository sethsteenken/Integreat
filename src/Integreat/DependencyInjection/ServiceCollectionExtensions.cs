using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Integreat
{
    public static class ServiceCollectionExtensions
    {
        public static IIntegreatServicesBuilder AddIntegreat(this IServiceCollection services)
        {
            Guard.IsNotNull(services, nameof(services));

            // TODO - load in integration settings from json file? look in appsettings with "Integreat" as the section? bring logic over from work lib

            // TODO - add microsoft logging if it doesn't exist?

            services.AddSingleton<IIntegrationSettings, IntegrationSettings>();
            services.AddSingleton<IFileStorage, SystemIOFileStorage>();
            services.AddSingleton<ISerializer, JsonNetSerializer>();
            services.AddSingleton<IProcessFactory, ProcessFactory>();
            services.AddSingleton<IWatcher, FileWatcher>();

            services.AddScoped<IDictionary<string, IProcessExecutableAdapter>>((serviceProvider) =>
            {
                return serviceProvider.GetServices<IProcessExecutableAdapter>()?.ToDictionary((adapter) => adapter.Type);
            });

            services.AddScoped<ProcessId>()
                .AddScoped<IProcessLogger, ProcessLogger>()
                .AddScoped<IIntegrationArchiver, IntegrationArchiver>()
                .AddScoped<IIntegrationFileHandler, IntegrationFileHandler>()
                .AddScoped<IExecutionPlanFactory, ExecutionPlanFactory>()
                .AddScoped<IProcessExecutor, ProcessExecutor>()
                .AddScoped<IProcess, Process>();

            return new IntegreatServicesBuilder(services);
        }
    }
}
