
namespace ShoppingTask.Domain.Tools.SignalR.Products
{
    public interface IProductHub
    {
        Task ReceiveShopHaveNewProducts(string Message , int ShopId, int notificationId);
        Task ReceiveNewProductsIdOfShop(List<int> ProductsId, int ShopId);
        Task ReceiveReservedProductId(List<int> ProductsId);
    }
}
