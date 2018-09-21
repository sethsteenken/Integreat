using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading;

namespace Integreat.CLI
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new CommandLineApplication();
            bool watching = false;
            IWatcher watcher = null;
            IServiceCollection services = new ServiceCollection();

            app.HelpOption();

            var optionFilePath = app.Option("-f|--file <FILEPATH>", 
                "The relative or absolute path to the integration package file.", 
                CommandOptionType.SingleValue)
                .IsRequired(errorMessage: "Filepath is required to run Integreat.");

            var optionWatch = app.Option("-w|--watch", "Set up file watcher for filepath.", CommandOptionType.NoValue);
            var optionWatchDuration = app.Option<int>("-wd|--watch-duration", "How long (in seconds) the file watcher should be active.", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                var filePath = optionFilePath.Value();
                watching = optionWatch.HasValue();

                var file = new Integreat.File(filePath);

                services.AddIntegreat()
                    .AddSettings(new IntegrationSettings()
                    {
                        DropDirectory = file.Directory,
                        DropFileName = file.FileName
                    });

                IServiceProvider serviceProvider = services.BuildServiceProvider();

                if (watching)
                {
                    Console.WriteLine($"Watching for file '{file.FileName}' in directory '{file.Directory}'...");

                    watcher = serviceProvider.GetRequiredService<IWatcher>();
                    watcher.Initialize();
                }
                else
                {
                    Console.WriteLine($"Executing Integreat using file at '{filePath}'...");

                    if (!System.IO.File.Exists(filePath))
                    {
                        Console.WriteLine($"File '{filePath}' not found.");
                        return 1;
                    }

                    IProcessFactory processFactory = serviceProvider.GetRequiredService<IProcessFactory>();
                    IProcess process = processFactory.Create();
                    process.Execute(filePath);
                }
                
                return 0;
            });

            var result = app.Execute(args);

            Console.WriteLine("execute result - " + result);
            
            if (result == 0)
            {
                if (watching)
                {
                    if (optionWatchDuration.HasValue() && optionWatchDuration.ParsedValue > 0)
                    {
                        Console.WriteLine($"Watching for {optionWatchDuration.ParsedValue} seconds...");
                        new AutoResetEvent(false).WaitOne(optionWatchDuration.ParsedValue * 1000);
                    }
                    else
                    {
                        Console.WriteLine($"Watching until dismissed...");
                        new AutoResetEvent(false).WaitOne();
                    }

                    Console.WriteLine("Watcher dismissed.");
                    watcher?.Dispose();
                }
            }

            return result;
        }
    }
}
