namespace Integreat.SQL
{
    public class ExecutableSQLTypeConfiguration : ExecutableConfiguration
    {
        public string ConnectionString { get; private set; }
        public string SqlCommandType { get; private set; }
    }
}
