using NestAlbania.Data;
using NestAlbania.Repositories;

namespace NestAlbania.Services;

public class NotificationService : INotificationService
{
    private readonly NotificationRepository _repository;

    public NotificationService(NotificationRepository repository)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(string userId)
    {
        return await _repository.GetUnreadNotificationsAsync(userId);
    }
    
    
    public async Task CreateNotification(string userId, string message)
    {
        await _repository.CreateNotification(userId, message);
    }
    
    public async Task MarkAsRead(string userId)
    {
        await _repository.MarkAsRead(userId);
    }
    
    public async Task DeleteNotification(int notificationId)
    {
        await _repository.DeleteNotification(notificationId);
    }
    
    
}


public interface INotificationService
{
    Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(string userId);
    Task CreateNotification(string userId, string message);
    Task MarkAsRead(string userId);
    Task DeleteNotification(int notificationId);
}