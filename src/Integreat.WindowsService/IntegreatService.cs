using Integreat.Batch;
using Integreat.Configuration;
using Integreat.CSharp;
using Integreat.Powershell;
using Integreat.SQL;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Integreat.WindowsService
{
    public partial class IntegreatService : InteractiveServiceBase
    {
        private IWatcher _folderWatcher;

        public IntegreatService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var serviceProvider = BuildServiceProvider();

            _folderWatcher = serviceProvider.GetRequiredService<IWatcher>();
            _folderWatcher.Initialize();
        }

        protected override void OnStop()
        {
            _folderWatcher?.Dispose();
        }

        private IServiceProvider BuildServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            // add your own logger/loggerfactory here - by default, Integreat will log to the console

            // register Integreat classes

            var integreatServicesBuilder = services.AddIntegreat(/*"my/drop/directory"*/);

            // if pulling settings from appsettings.json, using Integreat.Configuration

            integreatServicesBuilder.AddJsonConfigurationSettings();

            // OR add custom settings instance

            //integreatServicesBuilder.AddSettings(new IntegrationSettings()
            //{
            //    DropDirectory = "my/drop/directory",
            //    DropFileName = "deploy.zip",
            //    Archive = false
            //});

            // add executable types that could be referenced by the integration process (execution plan)
            integreatServicesBuilder
                    .AddCSharpPluginExecutable()
                    .AddPowershellExecutable()
                    .AddSQLExecutable()
                    .AddBatchExecutable();

            // add any other services here

            return services.BuildServiceProvider();
        }
    }
}
