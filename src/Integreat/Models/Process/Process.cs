using System;
using System.Collections.Generic;
using System.Text;

namespace Integreat
{
    public class Process : IProcess
    {
        public Process()
        {

        }

        public Guid Id => throw new NotImplementedException();

        public string Result => throw new NotImplementedException();

        public void Execute()
        {
            //if (Executables == null || Executables.Count == 0)
            //    throw new InvalidOperationException("There are no Executables registered in the Execution Plan.");

            //foreach (var executable in Executables)
            //{
            //    executable.Execute(new ExecutionContext(IntegrationDirectory, ExecutablesDirectory));
            //}
        }
    }
}
