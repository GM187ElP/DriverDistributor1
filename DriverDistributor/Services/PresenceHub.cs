namespace DriverDistributor.Services;

using Microsoft.AspNetCore.SignalR;

public class PresenceHub : Hub
{
    private readonly Presence _presence;

    public PresenceHub(Presence presence)
    {
        _presence = presence;
    }

    public override async Task OnConnectedAsync()
    {
        var username = Context.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(username))
        {
            _presence.UpdateLastSeen(username);
            await Clients.All.SendAsync("UserUpdated", username, DateTime.UtcNow);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var username = Context.User?.Identity?.Name;
        if (!string.IsNullOrEmpty(username))
        {
            _presence.UpdateLastSeen(username); // mark last seen at disconnect
            await Clients.All.SendAsync("UserUpdated", username, DateTime.UtcNow);
        }
        await base.OnDisconnectedAsync(exception);
    }
}
