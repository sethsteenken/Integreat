namespace Integreat.SQL
{
    public static class IntegreatServicesBuilderExtensions
    {
        public static IIntegreatServicesBuilder AddSQLExecutable(this IIntegreatServicesBuilder builder)
        {
            return builder.AddExecutableAdapter<SQLProcessExecutableAdapter>();
        }
    }
}
