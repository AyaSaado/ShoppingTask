namespace ShoppingTask.Domain.Services.Products;

public class SoftDeleteHandler(IUnitOfWork unitOfWork) : IRequestHandler<SoftDeleteRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(SoftDeleteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _unitOfWork.Products.GetOneAsync(p => p.Id == request.Id)
                                         .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
                return Result.Failure(new Error("404", "Product Not Found"));

            product.IsDeleted = true;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("400", ex.Message));
        }


    }
}
