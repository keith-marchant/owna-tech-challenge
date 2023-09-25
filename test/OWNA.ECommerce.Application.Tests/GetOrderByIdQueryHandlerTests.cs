using FluentAssertions;
using OWNA.ECommerce.Application.Entities;
using OWNA.ECommerce.Application.Exceptions;
using OWNA.ECommerce.Application.Interfaces;
using OWNA.ECommerce.Application.Queries.GetOrder;
using OWNA.ECommerce.Application.Shared.Enums;

namespace OWNA.ECommerce.Application.Tests;

public class GetOrderByIdQueryHandlerTests
{
    [Fact]
    public async Task GetOrderByIdQueryHandler_Handle_SucceedsWhenOrderExists()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var order = new Order(orderId, new Customer("Name", "Address", "Email@Email.com", "0411111111"),
            new Product("Name", "Description", 1), OrderStatus.Processing);
        var repoMock = new Mock<IOrderRepository>();
        repoMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(order);
        var query = new GetOrderByIdQuery(orderId);
        var handler = new GetOrderByIdQueryHandler(repoMock.Object);

        // Act
        var actual = await handler.Handle(query, CancellationToken.None);

        // Assert
        actual.OrderId.Should().Be(orderId);
    }

    [Fact]
    public async Task GetOrderByIdQueryHandler_Handle_ThrowsNotFoundWhenOrderMissing()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var repoMock = new Mock<IOrderRepository>();
        repoMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Order?)null);
        var query = new GetOrderByIdQuery(orderId);
        var handler = new GetOrderByIdQueryHandler(repoMock.Object);

        // Act
        var act = () => handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}