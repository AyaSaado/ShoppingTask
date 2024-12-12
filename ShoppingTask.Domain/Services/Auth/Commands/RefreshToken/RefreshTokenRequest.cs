using ShoppingTask.Core.Shared;

namespace ShoppingTask.Services.Auth
{
    public class RefreshTokenRequest : IRequest<Result<TokenRequest.Respone>>
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}
