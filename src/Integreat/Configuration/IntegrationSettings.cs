namespace Integreat
{
    public class IntegrationSettings : IIntegrationSettings
    {
        public string DropDirectory { get; set; }
        public string DropFileName { get; set; } = "Integration.zip";
        public string ExecutionPlanFileName { get; set; } = "Execution_Plan.json";
        public bool AllowExecutionPlanWithIntegration { get; set; } = true;
        public string ServiceWorkingDirectory { get; set; } = "__working";
        public int DropFileReadyTimeout { get; set; } = 300;
        public bool DeleteFromDropDirectory { get; set; } = true;
        public int ArchiveLimit { get; set; } = 10;
        public string ArchiveDirectory { get; set; } = "__archive";
        public bool Archive { get; set; } = true;
        public string NotificationEmail { get; set; }
        public string NotificationFrom { get; set; }
        public string OnCompleteDroppedFileName { get; set; }
        public bool CleanUpWorkspace { get; set; } = true;
    }
}
