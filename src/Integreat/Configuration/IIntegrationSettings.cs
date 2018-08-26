namespace Integreat
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
        bool Archive { get; }
        string NotificationEmail { get; }
        string NotificationFrom { get; }
        string OnCompleteDroppedFileName { get; }
        bool CleanUpWorkspace { get; }
        string PluginExecutorAppPath { get; }
    }
}
