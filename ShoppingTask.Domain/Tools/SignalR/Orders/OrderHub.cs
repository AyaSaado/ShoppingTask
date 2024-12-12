namespace ShoppingTask.Domain.Tools.SignalR.Orders;

[Authorize]
public  class OrderHub : Hub<IOrderHub>
{
    public override async Task OnConnectedAsync()
    {
        var user = Context.User;

        if (user?.Identity is ClaimsIdentity claimsIdentity)
        {
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
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

        }

        await base.OnDisconnectedAsync(exception);
    }

}
