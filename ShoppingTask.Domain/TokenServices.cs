using ShoppingTask.Services.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace ShoppingTask.Domain
{
    public class TokenServices(
        IUnitOfWork unitOfWork,
        IJwtProvider jwtProvider
    )
    {
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        
        public async Task<TokenRequest.Respone> GenerateToken(
            User user,
            List<string> roles,
            CancellationToken cancellationToken
        )
        {
            var Token = _jwtProvider.Generate(user, roles);

            Random r = new Random();

            var RefreshToken = new RefreshToken()
            {
                JwtId = Token.Id,
                Token = Guid.NewGuid().ToString() + r.NextDouble().ToString(),
                ExpiryDate = DateTime.UtcNow.AddMonths(1),
                UserId = user.Id,
            };

            await _unitOfWork.RefreshTokens.AddAsync(RefreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new TokenRequest.Respone()
            { Token = new JwtSecurityTokenHandler().WriteToken(Token), RefreshToken = RefreshToken.Token };
        }
    }
}
