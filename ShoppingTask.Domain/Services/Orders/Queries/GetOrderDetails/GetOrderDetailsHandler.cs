namespace ShoppingTask.Domain.Services.Orders;

public class GetOrderDetailsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrderDetailsRequest, List<GetOrderDetailsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<List<GetOrderDetailsResponse>> Handle(GetOrderDetailsRequest request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.OrderItems.Find(o => o.OrderId == request.OrderId, true)
                                           .Select(GetOrderDetailsResponse.Selector())
                                           .ToListAsync(cancellationToken);
    }
}
