namespace Integreat
{
    public interface IExecutionPlanFactory
    {
        IExecutionPlan Create(string processDirectory);
    }
}
