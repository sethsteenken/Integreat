namespace Integreat.Batch
{
    public static class IntegreatServicesBuilderExtensions
    {
        public static IIntegreatServicesBuilder AddBatchExecutable(this IIntegreatServicesBuilder builder)
        {
            return builder.AddExecutableAdapter<BatchProcessExecutableAdapter>();
        }
    }
}
