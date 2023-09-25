using Microsoft.Extensions.Caching.Memory;
using OWNA.ECommerce.Application.Entities;
using OWNA.ECommerce.Application.Interfaces;

namespace OWNA.ECommerce.Infrastructure.Persistence;

public class MemoryCacheOrderRepository : IOrderRepository
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheOrderRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task CreateOrderAsync(Order order, CancellationToken cancellationToken)
    {
        _memoryCache.Set(order.OrderId, order);
        return Task.CompletedTask;
    }

    public Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return _memoryCache.TryGetValue(orderId, out var order) ? Task.FromResult((Order?)order) : Task.FromResult<Order?>(null);
    }

    public Task UpdateOrder(Order order, CancellationToken cancellationToken)
    {
        _memoryCache.Set(order.OrderId, order);
        return Task.CompletedTask;
    }
}