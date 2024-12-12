namespace ShoppingTask.Domain.Services.Products;

public class GetProductByIdHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetProductByIdRequest, Result<GetProductByIdResponse?>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GetProductByIdResponse?>> Handle(
        GetProductByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var product = await _unitOfWork
            .Products.Find(p => p.Id == request.ProductId)
            .Select(GetProductByIdResponse.Selector())
            .FirstOrDefaultAsync(cancellationToken);

        if (product == null)
            return Result.Failure<GetProductByIdResponse?>(new Error("404", "Product Not Found"));

        return product;
    }
}
