namespace ShoppingTask.Domain.Services.Products;

public class FilterProductsHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<FilterProductsRequest, PaginationResponseDTO<FilterProductsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PaginationResponseDTO<FilterProductsResponse>> Handle(
        FilterProductsRequest request,
        CancellationToken cancellationToken
    )
    {
        return await _unitOfWork
            .Products.Find(p => request.Name == null || (p.Name.Contains(request.Name)), true)
            .Select(FilterProductsResponse.Selector())
            .PaginateAsync(cancellationToken, request.Page, request.size);
    }
}
