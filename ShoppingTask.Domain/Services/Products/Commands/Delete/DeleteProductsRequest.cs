namespace ShoppingTask.Domain.Services.Products;

public class DeleteProductsRequest : IRequest<Result>
{
    public List<int> ProductIds { get; set; } = new List<int>();
}
