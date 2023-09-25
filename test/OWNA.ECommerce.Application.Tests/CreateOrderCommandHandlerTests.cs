using FluentAssertions;
using OWNA.ECommerce.Application.Commands.CreateOrder;
using OWNA.ECommerce.Application.Commands.Dtos;
using OWNA.ECommerce.Application.Interfaces;
using OWNA.ECommerce.Application.Shared.Enums;

namespace OWNA.ECommerce.Application.Tests;

public class CreateOrderCommandHandlerTests
{
    [Fact] 
    public async Task CreateOrderCommandHandler_Handle_AssignsGuidSuccessfully()
    {
        // Arrange
        var command = new CreateOrderCommand(new CustomerDto("Name", "Address", "Email@Email.com", "0411111111"),
            new ProductDto("Name", "Description", 0.0f), OrderStatus.Pending);
        var commandHandler = new CreateOrderCommandHandler(new Mock<IOrderRepository>().Object);

        // Act
        var actual = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        actual.OrderId.Should().NotBeEmpty();
    }
}