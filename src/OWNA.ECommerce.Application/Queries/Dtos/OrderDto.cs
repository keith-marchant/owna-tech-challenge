using OWNA.ECommerce.Application.Shared.Enums;

namespace OWNA.ECommerce.Application.Queries.Dtos;

public record OrderDto(Guid OrderId, GetCustomerDto Customer, GetProductDto Product, OrderStatus Status);