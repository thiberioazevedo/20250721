using Microsoft.Extensions.DependencyInjection;

namespace Quack.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
