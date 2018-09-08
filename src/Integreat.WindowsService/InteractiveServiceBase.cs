namespace Integreat.WindowsService
{
    public abstract class InteractiveServiceBase : System.ServiceProcess.ServiceBase
    {
        protected InteractiveServiceBase()
        {
#if DEBUG
            ServiceName = $"{GetType().Name}";
#endif
        }

        internal void OnStartInteractive(string[] args)
        {
            OnStart(args);
        }

        internal void OnStopInteractive()
        {
            OnStop();
        }
    }
}
