using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integreat
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegreat(this IServiceCollection services)
        {
            Guard.IsNotNull(services, nameof(services));

            // TODO - load in integration settings from json file?

            services.AddSingleton<IIntegrationSettings, IntegrationSettings>();
            services.AddSingleton<IFileStorage, SystemIOFileStorage>();
            services.AddSingleton<ISerializer, JsonNetSerializer>();
            services.AddSingleton<IProcessFactory, ProcessFactory>();

            services.AddSingleton<IWatcher, FileWatcher>();

            // TODO - add adapters - how to register?


            // TODO - add microsoft logging

            return services;
        }
    }
}
