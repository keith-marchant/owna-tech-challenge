using FluentValidation;

namespace OWNA.ECommerce.Application.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotNull();
        RuleFor(x => x.Customer).NotNull();
        RuleFor(x => x.Product).NotNull();
        RuleFor(x => x.Customer.Name).NotEmpty();
        RuleFor(x => x.Customer.Address).NotEmpty();
        RuleFor(x => x.Customer.Phone).NotEmpty();
        RuleFor(x => x.Customer.Email).NotEmpty();
        RuleFor(x => x.Product.Name).NotEmpty();
        RuleFor(x => x.Product.Description).NotEmpty();
    }
}