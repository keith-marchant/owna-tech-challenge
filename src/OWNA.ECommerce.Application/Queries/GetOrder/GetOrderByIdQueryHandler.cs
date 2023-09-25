using MediatR;
using OWNA.ECommerce.Application.Exceptions;
using OWNA.ECommerce.Application.Interfaces;
using OWNA.ECommerce.Application.Queries.Dtos;

namespace OWNA.ECommerce.Application.Queries.GetOrder;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            throw new NotFoundException($"Unable to find orderId {request.OrderId}.");
        }

        return new OrderDto(order.OrderId,
            new GetCustomerDto(order.Customer.Name, order.Customer.Address, order.Customer.Email, order.Customer.Phone),
            new GetProductDto(order.Product.Name, order.Product.Description, order.Product.Price), order.Status);
    }
}