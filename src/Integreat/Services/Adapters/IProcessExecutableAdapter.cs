namespace Integreat
{
    public interface IProcessExecutableAdapter
    {
        string Type { get; }
        ProcessExecutable Build(dynamic configurationValues);
    }
}
