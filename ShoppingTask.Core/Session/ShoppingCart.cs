namespace ShoppingTask.Core.Session;

using Microsoft.Extensions.Caching.Memory;

public class ShoppingCart
{
    private readonly IMemoryCache _cache;

    public ShoppingCart(IMemoryCache cache)
    {
        _cache = cache;
    }

    public List<CartItem>? GetCartItems(string userId)
    {
        if (_cache.TryGetValue(userId, out List<CartItem>? cartItems))
        {
            return cartItems;
        }
        else
        {
            return new List<CartItem>();
        }
    }

    public void AddItem(string userId, CartItem item)
    {
        var cartItems = GetCartItems(userId);
        cartItems.Add(item);
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(
            TimeSpan.FromHours(1)
        );
        _cache.Set(userId, cartItems, cacheEntryOptions);
    }

    public void UpdateItem(string userId, CartItem item)
    {
        var cartItems = GetCartItems(userId);
        var existingItem = cartItems.Find(i => i.ProductId == item.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity = item.Quantity;
        }
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(
            TimeSpan.FromHours(1)
        );
        _cache.Set(userId, cartItems, cacheEntryOptions);
    }

    public void RemoveItem(string userId, int productId)
    {
        var cartItems = GetCartItems(userId);
        var itemToRemove = cartItems.Find(i => i.ProductId == productId);
        if (itemToRemove != null)
        {
            cartItems.Remove(itemToRemove);
            if (cartItems.Count == 0)
            {
                _cache.Remove(userId);
            }
            else
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(
                    TimeSpan.FromHours(1)
                );
                _cache.Set(userId, cartItems, cacheEntryOptions);
            }
        }
    }
}

public class CartItem
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
