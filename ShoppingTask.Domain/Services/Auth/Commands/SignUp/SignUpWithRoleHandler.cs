namespace ShoppingTask.Domain.Auth;

public class SignUpWithRoleHandler(IUnitOfWork unitOfWork) : IRequestHandler<SignUpWithRoleRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(SignUpWithRoleRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userUniqueWithUserName = await _unitOfWork.Users.GetOneAsync(u => u.UserName == request.SignUpRequest.UserName)                     
                                                    .FirstOrDefaultAsync(cancellationToken);

            if (userUniqueWithUserName is not null)
            {
                return Result.Failure(new Error("400", "This UserName is already exist"));
            }

            var userUniqueWithEmail = await _unitOfWork.Users.GetOneAsync(u => u.Email == request.SignUpRequest.Email)
                                               .FirstOrDefaultAsync(cancellationToken);

            if (userUniqueWithEmail is not null)
            {
                return Result.Failure(new Error("400", "This Email is already exist"));
            }


            var user = new User()
            {
                UserName = request.SignUpRequest.UserName,
                Email = request.SignUpRequest.Email,
            };

            var IsAdded = await _unitOfWork.Users.AddWithRole(user, request.Role.ToString(), request.SignUpRequest.Password);
         
            if (!IsAdded.Succeeded)
            {
                return  Result.Failure(new Error( "400" ,IsAdded.Errors.First().Description));
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return  Result.Success();

        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("400", ex.Message));
        }
    }
}
