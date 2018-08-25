namespace Integreat
{
    public interface IExecutableConfigurationFactory
    {
        ExecutableConfiguration Create(string type, dynamic configurationValues);
    }
}
