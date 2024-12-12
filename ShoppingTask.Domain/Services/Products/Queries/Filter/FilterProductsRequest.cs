namespace ShoppingTask.Domain.Services.Products;

public class FilterProductsRequest : IRequest<PaginationResponseDTO<FilterProductsResponse>>
{
    public string? Name { get; set; }
    public int Page { get; set; } = 1;
    public int size { get; set; } = 10;
}

public class FilterProductsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }

    public static Expression<Func<Product, FilterProductsResponse>> Selector() =>
        c =>
            new()
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
            };
}
