namespace Integreat
{
    public class IntegrationSettings : IIntegrationSettings
    {
        public string DropDirectory { get; set; }

        public string DropFileName { get; set; }

        public string ExecutionPlanFileName  { get; set; }
        public bool AllowExecutionPlanWithIntegration { get; set; }

        public string ServiceWorkingDirectory { get; set; }

        public int DropFileReadyTimeout { get; set; }

        public bool DeleteFromDropDirectory { get; set; }

        public int ArchiveLimit { get; set; }

        public string ArchiveDirectory { get; set; }

        public bool ArchiveIntegration { get; set; }

        public string NotificationEmail { get; set; }

        public string NotificationFrom { get; set; }

        public string OnCompleteDroppedFileName { get; set; }

        public bool DropFileOnComplete { get; set; }

        public bool CleanUpWorkspace { get; set; }

        public string PluginExecutorExePath { get; set; }
    }
}
