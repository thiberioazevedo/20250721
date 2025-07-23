using Quack.Application.Interfaces;
using Quack.Domain.Interfaces;
using Quack.Application.Services;
using Quack.Domain.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Quack.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILogParserService, LogParserService>();
            services.AddScoped<IEntityFactory, EntityFactory>();

            return services;
        }
    }
}
