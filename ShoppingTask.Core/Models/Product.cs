namespace ShoppingTask.Core.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Stock { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();

}
