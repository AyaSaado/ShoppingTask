namespace ShoppingTask.Domain.Services.Products;

public class UpdateProductRequest : IRequest<Result>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Stock { get; set; }
    public IFormFile? Image { get; set; }
}
