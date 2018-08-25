using System;
using System.Collections.Generic;
using System.Text;

namespace Integreat
{
    public class ExecutableConfigurationFactory : IExecutableConfigurationFactory
    {
        public ExecutableConfigurationFactory()
        {

        }

        public ExecutableConfiguration Create(string type, dynamic configurationValues)
        {
            Guard.IsNotNull(type, nameof(type));
            Guard.IsNotNull(configurationValues, nameof(configurationValues));

            switch (type)
            {
                case "":
                    break;
                default:
                    break;
            }


            return null;
        }
    }
}
