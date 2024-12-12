namespace ShoppingTask.Infrastructure.Repository.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        Users = new UserRepository(userManager, context);
        Orders = new BaseRepository<Order>(context);
        OrderItems = new BaseRepository<OrderItem>(context);
        Products = new BaseRepository<Product>(context);
        RefreshTokens = new BaseRepository<RefreshToken>(context);
    }

    public IUserRepository Users { get; private set; }
    public IBaseRepository<Order> Orders { get; private set; }
    public IBaseRepository<Product> Products { get; private set; }
    public IBaseRepository<RefreshToken> RefreshTokens { get; private set; }
    public IBaseRepository<OrderItem> OrderItems { get; private set; }

    public async void Dispose()
    {
        await _context.DisposeAsync();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
