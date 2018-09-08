﻿using Microsoft.Extensions.DependencyInjection;

namespace Integreat
{
    internal class IntegreatServicesBuilder : IIntegreatServicesBuilder
    {
        public IntegreatServicesBuilder(IServiceCollection services)
        {
            Guard.IsNotNull(services, nameof(services));

            Services = services;
        }

        public IServiceCollection Services { get; private set; }
    }
}
