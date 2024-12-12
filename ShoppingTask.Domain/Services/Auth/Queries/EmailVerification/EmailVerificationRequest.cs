namespace ShoppingTask.Domain.Services.Auth;

public class EmailVerificationRequest : IRequest<Result<string?>>
{
    public string Email { get; set; } = null!;
}
