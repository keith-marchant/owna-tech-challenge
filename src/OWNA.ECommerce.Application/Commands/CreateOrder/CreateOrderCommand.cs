using MediatR;
using OWNA.ECommerce.Application.Commands.Dtos;
using OWNA.ECommerce.Application.Shared.Enums;

namespace OWNA.ECommerce.Application.Commands.CreateOrder;

public record CreateOrderCommand(CustomerDto Customer, ProductDto Product, OrderStatus Status) : IRequest<CreateOrderDto>;