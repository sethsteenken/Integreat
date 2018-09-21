using McMaster.Extensions.CommandLineUtils;
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
            FileSystemWatcher watcher = null;

            app.HelpOption();

            var optionFilePath = app.Option("-f|--file <FILEPATH>", 
                "The relative or absolute path to the integration package file.", 
                CommandOptionType.SingleValue)
                .IsRequired(errorMessage: "Filepath is required to run Integreat.");

            var optionWatch = app.Option("-w|--watch", "Set up file watcher for filepath.", CommandOptionType.NoValue);

            app.OnExecute(() =>
            {
                var filePath = optionFilePath.Value();
                watching = optionWatch.HasValue();

                if (watching)
                {
                    Console.WriteLine($"Watching filepath '{filePath}'...");

                    var file = new File(filePath);

                    Console.WriteLine($"Watching for file '{file.FileName}' in directory '{file.Directory}'...");

                    watcher = new FileSystemWatcher(
                        file.Directory,
                        file.FileName)
                    {
                        NotifyFilter = NotifyFilters.LastWrite,
                        InternalBufferSize = 65536
                    };

                    watcher.Changed += new FileSystemEventHandler((sender, e) =>
                    {
                        watcher.EnableRaisingEvents = false;

                        Console.WriteLine("File changed - " + e.FullPath);

                        watcher.EnableRaisingEvents = true;
                    });

                    watcher.Error += new ErrorEventHandler((sender, e) =>
                    {
                        Console.WriteLine("exception - " + e.GetException());
                    });

                    watcher.EnableRaisingEvents = true;
                }
                else
                {
                    Console.WriteLine($"Executing filepath '{filePath}'...");
                }
                
                return 0;
            });

            var result = app.Execute(args);

            Console.WriteLine("execute result - " + result);
            
            if (result == 0)
            {
                if (watching)
                {
                    Console.WriteLine("waiting");
                    new AutoResetEvent(false).WaitOne(15000);
                    Console.WriteLine("after waitone");
                    watcher?.Dispose();
                }
            }

            return result;
        }
    }
}
