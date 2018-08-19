using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integreat
{
    public class ProcessFactory : IProcessFactory
    {
        public ProcessFactory()
        {

        }

        public IProcess Create()
        {
            //TODO - build dependencies for process and return correct process
            return new Process();
        }
    }
}
