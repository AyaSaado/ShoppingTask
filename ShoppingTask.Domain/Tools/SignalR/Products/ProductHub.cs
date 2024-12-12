namespace ShoppingTask.Domain.Tools.SignalR.Products;

[Authorize]
public class ProductHub : Hub<IProductHub>
{
    public override async Task OnConnectedAsync()
    {
        var user = Context.User;
        if (user?.Identity is ClaimsIdentity claimsIdentity)
        {
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
            await Groups.AddToGroupAsync(Context.ConnectionId, role!);
            
            if (userId != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId!);
            }
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var user = Context.User;

        if (user?.Identity is ClaimsIdentity claimsIdentity)
        {
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }

            var role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;

            if (role != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, role);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

}
