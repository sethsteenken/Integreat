﻿using Integreat.Batch;
using Integreat.CSharp;
using Integreat.SQL;
using Integreat.Powershell;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
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

            var optionWatch = app.Option("-w|--watch", "Optional. Set up file watcher for filepath.", CommandOptionType.NoValue);
            var optionWatchDuration = app.Option<int>("-wd|--watch-duration <DURATION_IN_SECONDS>", "Optional. How long (in seconds) the file watcher should be active.", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                var filePath = optionFilePath.Value();
                watching = optionWatch.HasValue();

                var file = new Integreat.File(filePath);

                if (!watching && !file.Exists)
                {
                    Console.WriteLine($"File '{file.FullPath}' not found.");
                    return 1;
                }

                services.AddIntegreat()
                    .AddBatchExecutable()
                    .AddPowershellExecutable()
                    .AddCSharpPluginExecutable()
                    .AddSQLExecutable()
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

                    serviceProvider.GetRequiredService<IProcessFactory>()
                            .Create()
                            .Execute(filePath);
                }
                
                return 0;
            });

            var result = app.Execute(args);

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

            Console.WriteLine($"Integreat process ended with result {result}.");

            return result;
        }
    }
}
