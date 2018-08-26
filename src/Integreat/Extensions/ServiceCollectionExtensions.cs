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

            // TODO - change the return type a custom services builder to allow for devs to easily add their own adapters, etc.

            // TODO - load in integration settings from json file? look in appsettings with "Integreat" as the section? bring logic over from work lib

            services.AddSingleton<IIntegrationSettings, IntegrationSettings>();
            services.AddSingleton<IFileStorage, SystemIOFileStorage>();
            services.AddSingleton<ISerializer, JsonNetSerializer>();
            services.AddSingleton<IProcessFactory, ProcessFactory>();
            services.AddSingleton<IWatcher, FileWatcher>();

            // TODO - add adapters - how to register? IDictionary or add multiple singletons for IProcessExecutableAdapter list?


            // TODO - add microsoft logging

            return services;
        }
    }
}
