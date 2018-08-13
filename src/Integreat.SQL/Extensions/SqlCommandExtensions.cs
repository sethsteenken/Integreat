using Integreat.Core;
using System.Data.SqlClient;

namespace Integreat.SQL
{
    public static class SqlCommandExtensions
    {
        public static void AddParameters(this SqlCommand command, ExecutableParameters parameters)
        {
            Guard.IsNotNull(command, nameof(command));

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
            }
        }
    }
}
