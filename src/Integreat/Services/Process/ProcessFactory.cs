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
            return new Process(null);
        }
    }
}
