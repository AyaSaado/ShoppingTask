
namespace ShoppingTask.Domain.Services.Orders;

public class GetAllByUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllByUserRequest, PaginationResponseDTO<GetAllByUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;   
    public async Task<PaginationResponseDTO<GetAllByUserResponse>> Handle(GetAllByUserRequest request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Orders.Find(o => o.UserId == request.UserId, true)
                                       .Select(GetAllByUserResponse.Selector())
                                       .PaginateAsync(cancellationToken, request.Page, request.size);
    }
}
