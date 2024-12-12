

namespace ShoppingTask.Domain.Tools.SignalR.Orders
{
    public interface IOrderHub
    {
        Task ReceiveOrderStatusUpdate(int orderId, string status,int notificationId);
    }
}
