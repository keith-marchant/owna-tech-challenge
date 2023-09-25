using OWNA.ECommerce.Application.Entities;

namespace OWNA.ECommerce.Application.Interfaces;

public interface IOrderRepository
{
    Task CreateOrderAsync(Order order, CancellationToken cancellationToken);
    Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken);
    Task UpdateOrder(Order order, CancellationToken cancellationToken);
}