using Microsoft.AspNetCore.SignalR;
using NestAlbania.Data;
using NestAlbania.Hub;
using NestAlbania.Repositories;

namespace NestAlbania.Services;

public class NotificationService : INotificationService
{
    private readonly NotificationRepository _repository;
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(NotificationRepository repository, IHubContext<NotificationHub> hubContext)
    {
        _repository = repository;
        _hubContext = hubContext;
    }


    public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(string userId)
    {
        return await _repository.GetUnreadNotificationsAsync(userId);
    }
    
    
    public async Task CreateNotification(string userId, string message)
    {
        await _repository.CreateNotification(userId, message);
        await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
    
    // public async Task MarkAsRead(string userId)
    // {
    //     await _repository.MarkAsRead(userId);
    // }
    
    public async Task<IEnumerable<Notification>> MarkAsReadAsync(string userId)
    {
        var markedNotifications = await _repository.MarkAsReadAsync(userId);
        await _hubContext.Clients.User(userId).SendAsync("NotificationsMarkedAsRead");
        return markedNotifications;
    }
    
    public async Task DeleteNotification(int notificationId)
    {
        await _repository.DeleteNotification(notificationId);
    }
    
    
    
    public async Task<IEnumerable<Notification>> GetAllNotificationsAsync(string userId)
    {
        return await _repository.GetAllNotificationsAsync(userId);
    }
    
    public async Task<int> GetUnreadNotificationCountAsync(string userId)
    {
        return await _repository.GetUnreadNotificationCountAsync(userId);
    }
}


public interface INotificationService
{
    Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(string userId);
    Task CreateNotification(string userId, string message);
    // Task MarkAsRead(string userId);
    
    Task<IEnumerable<Notification>> MarkAsReadAsync(string userId);
    Task DeleteNotification(int notificationId);
    
    Task<IEnumerable<Notification>> GetAllNotificationsAsync(string userId);
    
    Task<int> GetUnreadNotificationCountAsync(string userId);


}