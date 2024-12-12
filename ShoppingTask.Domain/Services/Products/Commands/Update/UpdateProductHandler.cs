
namespace ShoppingTask.Domain.Services.Products;

public class UpdateProductHandler(IUnitOfWork unitOfWork , IFileServices fileServices) : IRequestHandler<UpdateProductRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileServices _fileServices = fileServices;    
    public async Task<Result> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _unitOfWork.Products.GetOneAsync(p => p.Id == request.Id)
                                         .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
                return Result.Failure(new Error("404", "Product Not Found"));

            product.Name = request.Name!;
            product.Description = request.Description!;
            product.Price = request.Price;
            product.Stock = request.Stock;

           if(request.Image is not null)
            {
                _fileServices.Delete(product.ImageUrl);

                product.ImageUrl = await _fileServices.Upload(request.Image);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("400", ex.Message));
        }


    }
}
