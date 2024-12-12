namespace ShoppingTask.Infrastructure.Repository.Users;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager, AppDbContext context)
        : base(context)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<User>> GetAllByRole(string role)
    {
        return await _userManager.GetUsersInRoleAsync(role);
    }

    public async Task<IdentityResult> AddWithRole(User user, string role, string password)
    {
        IdentityResult identityResult = await _userManager.CreateAsync(user, password);

        if (!identityResult.Succeeded)
            return identityResult;

        identityResult = await _userManager.AddToRoleAsync(user, role);
        return identityResult;
    }

    public async Task<IdentityResult> AddWithRole(User user, ICollection<string> roles)
    {
        IdentityResult identityResult = await _userManager.CreateAsync(user);

        if (!identityResult.Succeeded)
            return identityResult;

        identityResult = await _userManager.AddToRolesAsync(user, roles);
        return identityResult;
    }

    public async Task<IdentityResult> TryModifyPassword(User user, string? newPassword)
    {
        //if (newPassword.IsNullOrEmpty())
        //    return IdentityResult.Failed();

        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        IdentityResult TrychangePass = await _userManager.ResetPasswordAsync(
            user,
            resetToken,
            newPassword!
        );

        return TrychangePass;
    }
}
