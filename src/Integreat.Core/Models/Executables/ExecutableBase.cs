using System;
using System.Collections.Generic;
using System.Text;

namespace Integreat
{
    public abstract class ExecutableBase : IExecutable
    {
        public abstract string Name { get; }

        public void Execute(ExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
