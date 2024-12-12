namespace ShoppingTask.Domain.Services.Products;

public class GetProductByIdRequest : IRequest<Result<GetProductByIdResponse?>>
{
    public int ProductId { get; set; }
}

public class GetProductByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Stock { get; set; }

    public string? ImageUrl { get; set; }

    public static Expression<Func<Product, GetProductByIdResponse>> Selector() =>
        c =>
            new()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                Stock = c.Stock,
                ImageUrl = c.ImageUrl,
            };
}
