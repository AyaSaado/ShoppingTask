namespace ShoppingTask.Domain.Services.Products;

public class AddProductRequest : IRequest<Result>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Stock { get; set; }
    public IFormFile? Image { get; set; }
}
