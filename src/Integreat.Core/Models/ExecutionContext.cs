namespace Integreat
{
    /// <summary>
    /// Information passed to each executable. Typically includes the associated directories of the given execution process.
    /// </summary>
    public sealed class ExecutionContext
    {
        internal ExecutionContext(string integrationDirectory, string executableDirectory)
        {
            IntegrationDirectory = integrationDirectory;
            ExecutablesDirectory = executableDirectory;
        }

        public string IntegrationDirectory { get; private set; }
        public string ExecutablesDirectory { get; private set; }
    }
}
