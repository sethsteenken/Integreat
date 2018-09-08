namespace Integreat.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var services = new InteractiveServiceBase[]
            {
                new IntegreatService()
            };

#if DEBUG
            ServiceRunner.RunInteractive(services);
#else
            ServiceBase.Run(services);
#endif
        }
    }
}
