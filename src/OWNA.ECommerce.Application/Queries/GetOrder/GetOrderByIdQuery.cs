using MediatR;
using OWNA.ECommerce.Application.Queries.Dtos;

namespace OWNA.ECommerce.Application.Queries.GetOrder;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<OrderDto>;