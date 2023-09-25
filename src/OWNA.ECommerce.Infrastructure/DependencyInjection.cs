using Microsoft.Extensions.DependencyInjection;
using OWNA.ECommerce.Application.Interfaces;
using OWNA.ECommerce.Infrastructure.Persistence;

namespace OWNA.ECommerce.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IOrderRepository, MemoryCacheOrderRepository>();
        
        return services;
    }
}
