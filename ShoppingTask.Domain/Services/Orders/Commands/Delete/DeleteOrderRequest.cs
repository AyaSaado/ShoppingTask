namespace ShoppingTask.Domain.Services.Orders;

public class DeleteOrderRequest : IRequest<Result>
{
    public List<int> OrderIds { get; set; } = new List<int>();
}
