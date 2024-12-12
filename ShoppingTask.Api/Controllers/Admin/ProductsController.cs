using ShoppingTask.Domain.Services.Products;

namespace ZoumouroudOrders.API.Controllers.Admin;

[Route("api/admin/[controller]")]
[ApiController]
[AppAuthorize(Roles.Admin)] // Just Admin Can Access it
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

    [HttpPost("Add")]
    [SwaggerResponse(StatusCodes.Status204NoContent, null)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(
        [FromForm] AddProductRequest command,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsFailure)
            return BadRequest(response.Error);

        return Created();
    }

    [HttpPut("Update")]
    [SwaggerResponse(StatusCodes.Status204NoContent, null)]
    [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(Error))]
    public async Task<IActionResult> Update(
        [FromForm] UpdateProductRequest command,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsFailure)
            return NotFound(response.Error);

        return NoContent();
    }

    [HttpDelete("Delete")]
    [SwaggerResponse(StatusCodes.Status204NoContent, null)]
    [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(Error))]
    public async Task<IActionResult> Delete(
        DeleteProductsRequest command,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsFailure)
            return BadRequest(response.Error);

        return NoContent();
    }

    [HttpDelete("SoftDelete")]
    [SwaggerResponse(StatusCodes.Status204NoContent, null)]
    [SwaggerResponse(StatusCodes.Status404NotFound, null, typeof(Error))]
    public async Task<IActionResult> SoftDelete(
        SoftDeleteRequest command,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(command, cancellationToken);
        if (response.IsFailure)
            return BadRequest(response.Error);

        return NoContent();
    }
}
