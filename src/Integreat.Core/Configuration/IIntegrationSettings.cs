namespace Integreat.Core
{
    public interface IIntegrationSettings
    {
        string DropDirectory { get; }
        string DropFileName { get; }
        string ExecutionPlanFileName { get; }
        bool AllowExecutionPlanWithIntegration { get; }
        string ServiceWorkingDirectory { get; }
        int DropFileReadyTimeout { get; }
        bool DeleteFromDropDirectory { get; }
        int ArchiveLimit { get; }
        string ArchiveDirectory { get; }
        bool ArchiveIntegration { get; }
        string NotificationEmail { get; }
        string NotificationFrom { get; }
        string OnCompleteDroppedFileName { get; }
        bool DropFileOnComplete { get; }
        bool CleanUpWorkspace { get; }
        string PluginExecutorExePath { get; }
    }
}
