namespace ShoppingTask.Domain.Services.Orders;

public class DeleteOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrderRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _unitOfWork.Orders.Find(p => request.OrderIds.Contains(p.Id))
                                               .ToListAsync(cancellationToken);

            if (entities.Count != request.OrderIds.Count)
                return Result.Failure(new Error("404", "Some Orders Not Found"));


            _unitOfWork.Orders.RemoveRange(entities);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("400", ex.Message));
        }
    }
}
