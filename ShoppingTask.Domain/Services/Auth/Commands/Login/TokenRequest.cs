
namespace ShoppingTask.Services.Auth
{
    public class TokenRequest 
    {
        public class Request : IRequest<Result<Respone>>
        {
            public required string UserName {  get; set; }
            public required string Password {  get; set; }
        }

        public class Respone
        {  
            public string Token { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
        }
    }
}
