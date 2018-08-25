namespace Integreat
{
    public interface IProcessExecutableAdapter
    {
        ProcessExecutable Build(dynamic configurationValues);
    }
}
