using MediatR;
using OWNA.ECommerce.Application.Commands.Dtos;
using OWNA.ECommerce.Application.Shared.Enums;

namespace OWNA.ECommerce.Application.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest
{
    public UpdateOrderCommand(CustomerDto customer, ProductDto product, OrderStatus status)
    {
        Customer = customer;
        Product = product;
        Status = status;
    }
    
    public Guid? OrderId { get; set; }

    public CustomerDto Customer { get; }
    public ProductDto Product { get; }
    public OrderStatus Status { get; }
}