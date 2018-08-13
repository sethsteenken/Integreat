using System.Collections.Generic;
using System.Linq;

namespace Integreat.Core
{
    public class ExecutableParameters : Dictionary<string, string>
    {
        public override string ToString()
        {
            if (Count <= 0)
                return string.Empty;

            return string.Join(",", this.Select(p => $"{p.Key}={p.Value}")).Trim();
        }
    }
}
