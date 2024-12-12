namespace ShoppingTask.Domain.Auth;

public class SignUpWithRoleRequest : IRequest<Result>
{
    public SignUpRequest SignUpRequest { get; }
    public Roles Role { get; }

    public SignUpWithRoleRequest(SignUpRequest signUpRequest, Roles role)
    {
        SignUpRequest = signUpRequest;
        Role = role;
    }
}

public class SignUpRequest
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
