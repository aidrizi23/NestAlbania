using Microsoft.AspNetCore.SignalR;

namespace NestAlbania.Hub;

public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
{
    public async Task SendNotification(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
}