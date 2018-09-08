using Microsoft.Extensions.DependencyInjection;

namespace Integreat
{
    public class ProcessFactory : IProcessFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ProcessFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IProcess Create()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                return scope.ServiceProvider.GetRequiredService<IProcess>();
            }
        }
    }
}
