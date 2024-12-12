using Microsoft.AspNetCore.Identity;
using ShoppingTask.Domain;

namespace ShoppingTask.Services.Auth
{
    public class LoginHandler : IRequestHandler<TokenRequest.Request, Result<TokenRequest.Respone>>
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenServices _services;

        public LoginHandler(
            UserManager<User> userManager,
            IJwtProvider jwtProvider,
            IUnitOfWork unitOfWork
        )
        {
            _userManager = userManager;
            _services = new TokenServices(unitOfWork, jwtProvider);
        }

        public async Task<Result<TokenRequest.Respone>> Handle(
            TokenRequest.Request request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var UserExist = await _userManager.FindByNameAsync(request.UserName);

                if (UserExist == null)
                {
                    return Result.Failure<TokenRequest.Respone>(new Error("404", "User Not Found"));
                }

                var IsPasswordMatch = await _userManager.CheckPasswordAsync(
                    UserExist,
                    request.Password
                );
                if (!IsPasswordMatch)
                {
                    return Result.Failure<TokenRequest.Respone>(new Error("400", "Wrong Password"));
                }

                var roles = await _userManager.GetRolesAsync(UserExist);

                return await _services.GenerateToken(UserExist, roles.ToList(), cancellationToken);
            }
            catch (Exception ex)
            {
                return Result.Failure<TokenRequest.Respone>(new Error("400", ex.Message));
            }
        }
    }
}
