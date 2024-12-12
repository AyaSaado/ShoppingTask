using ShoppingTask.Domain.Services.Orders;

namespace ShoppingTask.Api.Controllers.Public;

[Route("api/public/[controller]")]
[ApiController]
[Authorize]  
// Can Access it By All Authenticated User 
public class OrdersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("GetAllByUser")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(PaginationResponseDTO<GetAllByUserResponse>))]
    public async Task<IActionResult> Get([FromQuery] GetAllByUserRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }


    [HttpGet("GetOrderDetails")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(List<GetOrderDetailsResponse>))]
    public async Task<IActionResult> Get([FromQuery] GetOrderDetailsRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("Add")]
    [SwaggerResponse(StatusCodes.Status204NoContent, null)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(
        [FromBody] AddOrderRequest command,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsFailure)
            return BadRequest(response.Error);

        return Created();
    }

    // We Can use in scenario when we add status to order then if
    // the status is pending the user can delete it ^_^
    [HttpDelete("Delete")]
    [SwaggerResponse(StatusCodes.Status204NoContent, null)]
    [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(Error))]
    public async Task<IActionResult> Delete(DeleteOrderRequest command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsFailure)
            return BadRequest(response.Error);

        return NoContent();
    }

}
