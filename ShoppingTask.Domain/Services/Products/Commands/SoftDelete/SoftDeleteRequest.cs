namespace ShoppingTask.Domain.Services.Products;

public class SoftDeleteRequest : IRequest<Result>
{
    public int Id { get; set; }
}
