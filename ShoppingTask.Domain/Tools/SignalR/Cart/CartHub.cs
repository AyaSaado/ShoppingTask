namespace ShoppingTask.Domain.Hubs;

[Authorize]
public class CartHub : Hub
{
    private readonly ShoppingCart _session;

    public CartHub(ShoppingCart sessionStore)
    {
        _session = sessionStore;
    }

    public async Task GetCartItems()
    {
        var user = Context.User;

        if (user?.Identity is ClaimsIdentity claimsIdentity)
        {
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                var cartItems = _session.GetCartItems(userId);
                await Clients.Caller.SendAsync("ReceiveCartItems", cartItems);
            }
        }
    }
    public async Task AddItem(CartItem item)
    {
        var user = Context.User;

        if (user?.Identity is ClaimsIdentity claimsIdentity)
        {
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                _session.AddItem(userId, item);
                await Clients.Caller.SendAsync("ItemAdded", item);
            }
        }
       
    }

    public async Task UpdateItem(CartItem item)
    {
        var user = Context.User;

        if (user?.Identity is ClaimsIdentity claimsIdentity)
        {
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                _session.UpdateItem(userId, item);
                await Clients.Caller.SendAsync("ItemUpdated", item);
            }
        }
      
    }

    public async Task RemoveItem(int productId)
    {
        var user = Context.User;

        if (user?.Identity is ClaimsIdentity claimsIdentity)
        {
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                _session.RemoveItem(userId, productId);
                await Clients.Caller.SendAsync("ItemRemoved", productId);
            }
        }
       
    }
}