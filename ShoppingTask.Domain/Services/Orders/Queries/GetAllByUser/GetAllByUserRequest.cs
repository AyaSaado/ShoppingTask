namespace ShoppingTask.Domain.Services.Orders;

public class GetAllByUserRequest : IRequest<PaginationResponseDTO<GetAllByUserResponse>>
{
    public Guid UserId { get; set; }
    public int Page { get; set; } = 1;
    public int size { get; set; } = 10;
}
public class GetAllByUserResponse
{
    public int OrderId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }

    public static Expression<Func<Order, GetAllByUserResponse>> Selector() => c
         => new()
         {
             OrderId = c.Id,
             Date = c.Date,
             TotalAmount = c.TotalAmount
             
         };
}

