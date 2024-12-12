namespace ShoppingTask.Infrastructure.Repository.Users;

public interface IUserRepository : IBaseRepository<User>
{
    Task<IEnumerable<User>> GetAllByRole(string role);
    Task<IdentityResult> AddWithRole(User user, string role, string password);
    Task<IdentityResult> AddWithRole(User user, ICollection<string> roles);

    // Task<bool> IsEmailExist<User>(string email, Guid? id = null);
    Task<IdentityResult> TryModifyPassword(User user, string? newPassword);
}
