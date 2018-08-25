namespace Integreat
{
    public class CSharpPluginProcessExecutableAdapter : IProcessExecutableAdapter
    {
        public CSharpPluginProcessExecutableAdapter()
        {

        }

        public ProcessExecutable Build(dynamic configurationValues)
        {
            // TODO - validate dynamic properties
            // TODO - pass correct properties
            var configuration = new ExecutableCSharpTypeConfiguration("", 0, null, "typename", "assemblyname");
            var executable = new CSharpPluginExecutable(configuration.TypeName, configuration.AssemblyName, "pluginexecutorapppath");

            return new ProcessExecutable(executable, configuration);
        }
    }
}
