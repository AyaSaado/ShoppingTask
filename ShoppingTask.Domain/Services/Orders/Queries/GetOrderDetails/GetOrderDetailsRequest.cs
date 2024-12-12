namespace ShoppingTask.Domain.Services.Orders;

public class GetOrderDetailsRequest : IRequest<List<GetOrderDetailsResponse>>
{
    public int OrderId { get; set; }
}

public class GetOrderDetailsResponse
{
    public int OrderItemId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string? ProductImageUrl { get; set; }

    public static Expression<Func<OrderItem, GetOrderDetailsResponse>> Selector() =>
        c =>
            new()
            {
                OrderItemId = c.Id,
                Price = c.Price,
                Quantity = c.Quantity,
                ProductId = c.ProductId,
                ProductName = c.Product.Name,
                ProductImageUrl = c.Product.ImageUrl,
            };
}
