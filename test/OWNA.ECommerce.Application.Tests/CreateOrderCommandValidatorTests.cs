using FluentValidation.TestHelper;
using OWNA.ECommerce.Application.Commands.CreateOrder;
using OWNA.ECommerce.Application.Commands.Dtos;
using OWNA.ECommerce.Application.Shared.Enums;

namespace OWNA.ECommerce.Application.Tests;

public class CreateOrderCommandValidatorTests
{
    private readonly CreateOrderCommandValidator _validator = new CreateOrderCommandValidator();
    
    [Fact]
    public void CreateOrderCommandValidator_Validate_FailsWithNullName()
    {
        // Arrange
        var command = new CreateOrderCommand(new CustomerDto(null, "Address", "Email", "Phone"),
            new ProductDto("Name", "Description", 1), OrderStatus.Pending);

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Customer.Name);
    }
}