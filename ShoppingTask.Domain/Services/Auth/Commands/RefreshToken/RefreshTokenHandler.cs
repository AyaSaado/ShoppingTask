using Microsoft.AspNetCore.Identity;
using ShoppingTask.Domain;
using System.IdentityModel.Tokens.Jwt;

namespace ShoppingTask.Services.Auth
{
    
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, Result<TokenRequest.Respone>>
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly TokenServices _services;
        public RefreshTokenHandler(TokenServices service, IJwtProvider jwtProvider, IUnitOfWork unitOfWork, UserManager<User> userManager, IOptions<JwtBearerOptions> jwtBearerOptions)
        {

            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _services = service;
            _tokenValidationParameters = jwtBearerOptions.Value.TokenValidationParameters;

    }

    public async Task<Result<TokenRequest.Respone>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var JwtTokenHandler = new JwtSecurityTokenHandler();

                _tokenValidationParameters.ValidateLifetime = false; 
                var TokenInVarification = JwtTokenHandler.ValidateToken(request.Token, _tokenValidationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                        return Result.Failure<TokenRequest.Respone>(new Error("400", "Invalid Token"));

                }else
                    return Result.Failure<TokenRequest.Respone>(new Error("400", "Invalid Request1"));

              
                var StoredToken = await _unitOfWork.RefreshTokens.GetOneAsync(r => r.Token == request.RefreshToken).FirstOrDefaultAsync(cancellationToken);

                if (StoredToken == null)
                {
                    return Result.Failure<TokenRequest.Respone>(new Error("400", "Invalid RefreshToken"));
                }

                var jti = TokenInVarification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)!.Value;

                if (StoredToken.JwtId != jti)
                {
                    return Result.Failure<TokenRequest.Respone>(new Error("400", "Invalid Token"));
                }
                if (StoredToken.ExpiryDate < DateTime.UtcNow)
                {
                    return Result.Failure<TokenRequest.Respone>(new Error("400", "Expired RefreshToken"));
                }

                var user = await _unitOfWork.Users.GetOneAsync(u => u.Id == StoredToken.UserId).FirstOrDefaultAsync(cancellationToken);
                                                 

                if(user is null)
                {
                    return Result.Failure<TokenRequest.Respone>(new Error("400", "Not Found User"));
                }

                _unitOfWork.RefreshTokens.Remove(StoredToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var roles = await _userManager.GetRolesAsync(user);

                return await _services.GenerateToken(user, roles.ToList(), cancellationToken);

            }
            catch (Exception)
            {
                return Result.Failure<TokenRequest.Respone>(new Error("400", "Invalid Request"));
            }


        }

   
    }
}
