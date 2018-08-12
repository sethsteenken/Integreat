using System.Collections.Generic;
using System.Linq;

namespace Integreat.Core
{
    public class ExecutableParameters : List<ExecutableParameter>
    {
        public override string ToString()
        {
            if (Count <= 0)
                return string.Empty;

            return string.Join(",", this.Select(p => $"{p.Name}={p.ToParamString()}")).Trim();
        }
    }
}
