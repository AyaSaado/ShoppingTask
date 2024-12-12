namespace ShoppingTask.Domain.Services.Orders;

public class AddOrderHandler(IUnitOfWork unitOfWork, IMapper mapper ,IMailService mailService) : IRequestHandler<AddOrderRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IMailService _mailService = mailService;         
    public async Task<Result> Handle(AddOrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.Users.GetOneAsync(u => u.Id == request.UserId, true)
                                              .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
                return Result.Failure(new Error("404", "User Not Found"));


            var p = await _unitOfWork.Products.GetOneAsync(u => u.Id == request.OrderItemDTOs.FirstOrDefault().ProductId, true)
                                              .FirstOrDefaultAsync(cancellationToken);
            if (p == null)
                return Result.Failure(new Error("404", "p Not Found"));


            var order = new Order()
            {
                Date = request.Date,
                TotalAmount = request.OrderItemDTOs.Sum(oi => oi.Price * oi.Quantity),
                UserId = request.UserId,
                OrderItems = _mapper.Map<List<OrderItem>>(request.OrderItemDTOs)
            };

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Fire and Forget Mechanism To Continue executing the handler without wait the mail to be send
            // I want to start this asynchronous operation, but I don't care when it finishes.

            _ = SendOrderConfirmationEmailAsync(order, user.Email!);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("400", ex.Message));
        }
    }
    private async Task SendOrderConfirmationEmailAsync(Order order, string userEmail)
    {
        try
        {
            var email = new EmailDTO
            (
                To: userEmail,
                Subject: "Order Confirmation",
                Body: $"Your order {order.Id} has been successfully placed."
            );

            await _mailService.SendEmail(email);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}
