namespace Integreat
{
    public sealed class ProcessExecutable
    {
        public ProcessExecutable(IExecutable executable, ExecutableConfiguration configuration)
        {
            Guard.IsNotNull(executable, nameof(executable));
            Guard.IsNotNull(configuration, nameof(configuration));

            Executable = executable;
            Configuration = configuration;
        }

        public IExecutable Executable { get; private set; }
        public ExecutableConfiguration Configuration { get; private set; }
    }
}
