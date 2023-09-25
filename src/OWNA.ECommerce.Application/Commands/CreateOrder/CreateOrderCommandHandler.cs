using MediatR;
using OWNA.ECommerce.Application.Commands.Dtos;
using OWNA.ECommerce.Application.Entities;
using OWNA.ECommerce.Application.Interfaces;

namespace OWNA.ECommerce.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderDto>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<CreateOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(Guid.NewGuid(),
            new Customer(request.Customer.Name!, request.Customer.Address!, request.Customer.Email!,
                request.Customer.Phone!),
            new Product(request.Product.Name!, request.Product.Description!, request.Product.Price), request.Status);
        await _orderRepository.CreateOrderAsync(order, cancellationToken);
        return new CreateOrderDto(order.OrderId);
    }
}