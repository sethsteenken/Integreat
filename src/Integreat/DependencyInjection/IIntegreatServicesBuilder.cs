using Microsoft.Extensions.DependencyInjection;

namespace Integreat
{
    /// <summary>
    /// Defines a contract for adding Integreat services, executables, and adapters.
    /// </summary>
    public interface IIntegreatServicesBuilder
    {
        IServiceCollection Services { get; }
    }
}
