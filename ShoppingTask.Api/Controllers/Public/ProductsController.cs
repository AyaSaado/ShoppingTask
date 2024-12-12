using ShoppingTask.Domain.Services.Products;

namespace ZoumouroudOrders.API.Controllers.Public;

[Route("api/public/[controller]")]
[ApiController]
// Anonymous Users Can Use it

public class ProductsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("Get")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(GetProductByIdResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(Error))]
    public async Task<IActionResult> Get(
        [FromQuery] GetProductByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (response.IsFailure)
            return NotFound(response.Error);

        return Ok(response.Value);
    }

    [HttpGet("Filter")]
    [SwaggerResponse(
        StatusCodes.Status200OK,
        null,
        typeof(PaginationResponseDTO<FilterProductsResponse>)
    )]
    public async Task<IActionResult> Filter(
        [FromQuery] FilterProductsRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
