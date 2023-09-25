using MediatR;
using OWNA.ECommerce.Application.Entities;
using OWNA.ECommerce.Application.Interfaces;

namespace OWNA.ECommerce.Application.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.OrderId!.Value,
            new Customer(request.Customer.Name!, request.Customer.Address!, request.Customer.Email!,
                request.Customer.Phone!),
            new Product(request.Product.Name!, request.Product.Description!, request.Product.Price), request.Status);
        await _orderRepository.UpdateOrder(order, cancellationToken);
    }
}