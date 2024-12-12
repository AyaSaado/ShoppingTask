using ShoppingTask.Domain.Services.Auth;

namespace ShoppingTask.Api.Controllers.Public;

[Route("api/public/[controller]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("EmailVerification")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(string))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(Error))]
    public async Task<IActionResult> EmailVerification(
        [FromQuery] EmailVerificationRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(request, cancellationToken);
        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }

        return Ok(response.Value);
    }

    [HttpPost("SignUserUp")]
    [SwaggerResponse(StatusCodes.Status204NoContent, null)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(Result))]
    public async Task<IActionResult> SignUp(
        SignUpRequest request,
        CancellationToken cancellationToken
    )
    {
        var role = Roles.User;
        var signUpWithRoleRequest = new SignUpWithRoleRequest(request, role);

        var response = await _mediator.Send(signUpWithRoleRequest, cancellationToken);
        if (response.IsFailure)
        {
            return BadRequest(response);
        }
        return NoContent();
    }

    [HttpPost("Login")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(TokenRequest.Respone))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(Error))]
    public async Task<IActionResult> Login(
        TokenRequest.Request request,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }
        return Ok(response.Value);
    }

    [HttpPost("RefreshToken")]
    [SwaggerResponse(StatusCodes.Status200OK, null, typeof(TokenRequest.Respone))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(Error))]
    public async Task<IActionResult> RefreshToken(
        RefreshTokenRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (response.IsFailure)
            return BadRequest(response.Error);

        return Ok(response.Value);
    }
}
