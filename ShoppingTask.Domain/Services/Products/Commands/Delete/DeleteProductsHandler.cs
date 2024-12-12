namespace ShoppingTask.Domain.Services.Products;

public class DeleteProductsHandler(IUnitOfWork unitOfWork, IFileServices fileServices) : IRequestHandler<DeleteProductsRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileServices _fileServices = fileServices;   
    public async Task<Result> Handle(DeleteProductsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _unitOfWork.Products.Find(p => request.ProductIds.Contains(p.Id))
                                               .ToListAsync(cancellationToken);

            if (entities.Count != request.ProductIds.Count)
                return Result.Failure(new Error("404", "Some Products Not Found"));


            _fileServices.Delete(entities.Select(p => p.ImageUrl).ToList());

            _unitOfWork.Products.RemoveRange(entities);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("400", ex.Message));
        }
    }
}
