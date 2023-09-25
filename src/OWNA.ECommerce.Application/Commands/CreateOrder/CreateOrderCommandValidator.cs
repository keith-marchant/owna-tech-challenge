﻿using FluentValidation;

namespace OWNA.ECommerce.Application.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
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