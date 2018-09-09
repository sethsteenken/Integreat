using System;
using System.Threading;

namespace Integreat.WindowsService
{
    internal static class ServiceRunner
    {
        public static void RunInteractive(InteractiveServiceBase[] servicesToRun)
        {
            Console.WriteLine("Services running in interactive mode.");
            Console.WriteLine();

            foreach (InteractiveServiceBase service in servicesToRun)
            {
                Console.Write("Starting {0}...", service.ServiceName);
                service.OnStartInteractive(new string[] { });
                Console.Write("Started");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to stop the services and end the process...");
            Console.ReadKey();
            Console.WriteLine();

            foreach (InteractiveServiceBase service in servicesToRun)
            {
                Console.Write("Stopping {0}...", service.ServiceName);
                service.OnStopInteractive();
                Console.WriteLine("Stopped");
            }

            Console.WriteLine("All services stopped.");

            // Keep the console alive for a 1/2 second to allow the user to see the message.
            Thread.Sleep(500);
        }
    }
}
