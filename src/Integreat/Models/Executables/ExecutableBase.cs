using Integreat.Internal;
using Integreat.Core;
using System;
using System.Threading.Tasks;

namespace Integreat
{
    public abstract class ExecutableBase : IExecutable
    {
        public abstract string Name { get; }

        protected abstract void OnExecute(ExecutableContext context);

        public void Execute(ExecutableContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            context.LogStartupHeading(Name);

            if (context.Timeout > 0)
            {
                var task = Task.Run(() => OnExecute(context));
                if (!task.Wait(TimeSpan.FromSeconds(context.Timeout)))
                    throw new TimeoutException($"Executable {Name} failed to finish executing within allotted timeout of {context.Timeout} seconds.");
            }
            else
            {
                OnExecute(context);
            }

            context.Log($"** Executable {Name} completed. **");
        }
    }
}
