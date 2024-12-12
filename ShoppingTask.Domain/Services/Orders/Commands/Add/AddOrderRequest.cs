namespace ShoppingTask.Domain.Services.Orders;

public class AddOrderRequest : IRequest<Result>
{
    public DateTime Date { get; set; }
    public Guid UserId { get; set; }
    public List<OrderItemDTO> OrderItemDTOs { get; set; } = new();
}
