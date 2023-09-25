using OWNA.ECommerce.Application.Shared.Enums;

namespace OWNA.ECommerce.Application.Entities;

public record Order(Guid OrderId, Customer Customer, Product Product, OrderStatus Status);