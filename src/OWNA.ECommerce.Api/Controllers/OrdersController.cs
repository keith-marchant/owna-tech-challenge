using MediatR;
using Microsoft.AspNetCore.Mvc;
using OWNA.ECommerce.Api.ProblemDetails;
using OWNA.ECommerce.Application.Commands.CreateOrder;
using OWNA.ECommerce.Application.Commands.Dtos;
using OWNA.ECommerce.Application.Commands.UpdateOrder;
using OWNA.ECommerce.Application.Queries.Dtos;
using OWNA.ECommerce.Application.Queries.GetOrder;

namespace OWNA.ECommerce.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class OrdersController
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(CreateOrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnhandledExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<CreateOrderDto>> Create([FromBody] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(command, cancellationToken);
        return new CreatedResult(new Uri($"orders/{order.OrderId}", UriKind.Relative), order);
    }
    
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UnhandledExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpGet("{orderId}")]
    public async Task<ActionResult<OrderDto>> GetById([FromRoute]string orderId, CancellationToken cancellationToken)
    {
        return new OkObjectResult(await _mediator.Send(new GetOrderByIdQuery(Guid.Parse(orderId)), cancellationToken));
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequestProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UnhandledExceptionProblemDetails), StatusCodes.Status500InternalServerError)]
    [HttpPut("{orderId}")]
    public async Task<IActionResult> Update([FromRoute] Guid orderId, [FromBody] UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        command.OrderId = orderId;

        await _mediator.Send(command, cancellationToken);
        return new NoContentResult();
    }
}