namespace ShoppingTask.Infrastructure.Repository.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IBaseRepository<Order> Orders { get; }
    IBaseRepository<OrderItem> OrderItems { get; }
    IBaseRepository<Product> Products { get; }
    IBaseRepository<RefreshToken> RefreshTokens { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
