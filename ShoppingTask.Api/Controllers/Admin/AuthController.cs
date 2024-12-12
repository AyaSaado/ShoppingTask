namespace ShoppingTask.API.Controllers;

[Route("api/admin/[controller]")]
[ApiController]
// Lock Just By Admin Api Key 
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpPost("SignAdminUp")]
    [SwaggerResponse(StatusCodes.Status204NoContent, null)]
    [SwaggerResponse(StatusCodes.Status400BadRequest , null ,typeof(Result))]
    public async Task<IActionResult> SignUp(SignUpRequest request,CancellationToken cancellationToken)
    {

        var role = Roles.Admin; 
        var signUpWithRoleRequest = new SignUpWithRoleRequest(request, role);

        var response = await _mediator.Send(signUpWithRoleRequest, cancellationToken);


        if (response.IsFailure)
        {
            return BadRequest(response);
        }
        return NoContent();
    }

    
}
