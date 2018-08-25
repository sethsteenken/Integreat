namespace Integreat
{
    public sealed class ProcessExecutable
    {
        public ProcessExecutable(IExecutable executable, ExecutableConfiguration configuration)
        {
            Executable = executable;
            Configuration = configuration;
        }

        public IExecutable Executable { get; private set; }
        public ExecutableConfiguration Configuration { get; private set; }
    }
}
