using FluentAssertions;
using Moq;
using OWNA.ECommerce.Application.Commands.Dtos;
using OWNA.ECommerce.Application.Commands.UpdateOrder;
using OWNA.ECommerce.Application.Entities;
using OWNA.ECommerce.Application.Exceptions;
using OWNA.ECommerce.Application.Interfaces;
using OWNA.ECommerce.Application.Shared.Enums;

namespace OWNA.ECommerce.Application.Tests;

public class UpdateOrderCommandHandlerTests
{
    [Fact]
    public async Task UpdateOrderCommandHandler_Handle_UpdatesOrderWhenPresent()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var originalOrder = new Order(orderId, new Customer("Name", "Address", "Email@Email.com", "0400000000"),
            new Product("Name", "Description", 1), OrderStatus.Pending);
        var command = new UpdateOrderCommand(new CustomerDto("Name", "Address", "Email@Email.com", "04111111111"),
            new ProductDto("Name", "Description", 1), OrderStatus.Processing);
        command.OrderId = orderId;
        var mockRepo = new Mock<IOrderRepository>();
        mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(originalOrder);
        mockRepo.Setup(x => x.UpdateOrder(It.IsAny<Order>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();
        var handler = new UpdateOrderCommandHandler(mockRepo.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepo.Verify();
    }

    [Fact]
    public async Task UpdateOrderCommandHandler_Handle_ThrowsWhenOrderMissing()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var command = new UpdateOrderCommand(new CustomerDto("Name", "Address", "Email@Email.com", "04111111111"),
            new ProductDto("Name", "Description", 1), OrderStatus.Processing);
        command.OrderId = orderId;
        var mockRepo = new Mock<IOrderRepository>();
        mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?)null);
        var handler = new UpdateOrderCommandHandler(mockRepo.Object);
        
        // Act
        var act = () => handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}