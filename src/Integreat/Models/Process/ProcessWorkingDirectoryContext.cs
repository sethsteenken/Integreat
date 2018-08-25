namespace Integreat
{
    public class ProcessWorkingDirectoryContext
    {
        public ProcessWorkingDirectoryContext(string workingDirectory, string processDirectory)
        {
            Guard.IsNotNull(workingDirectory, nameof(workingDirectory));
            Guard.IsNotNull(processDirectory, nameof(processDirectory));

            WorkingDirectory = workingDirectory;
            ProcessDirectory = processDirectory;
        }

        public string WorkingDirectory { get; private set; }
        public string ProcessDirectory { get; private set; }
    }
}
