namespace ShoppingTask.Domain.Services.Products;

public class AddProductHandler(IUnitOfWork unitOfWork, IFileServices fileServices)
    : IRequestHandler<AddProductRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileServices _fileServices = fileServices;

    public async Task<Result> Handle(AddProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = await _fileServices.Upload(request.Image),
                Price = request.Price,
                Stock = request.Stock,
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("400", ex.Message));
        }
    }
}
