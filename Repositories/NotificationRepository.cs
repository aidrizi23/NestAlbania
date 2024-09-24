using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;

namespace NestAlbania.Repositories;

public class NotificationRepository
{
    private readonly ApplicationDbContext _context;
    
    public NotificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(string userId)
    {
        return await _context.Notifications.AsNoTracking()
            .Where(n => n.UserId == userId && n.IsRead == false)
            .ToListAsync();
    }
    
    public async Task CreateNotification(string userId, string message)
    {
        var notification = new Notification
        {
            UserId = userId,
            Message = message,
            CreatedOn = DateTime.UtcNow,
            IsRead = false
        };

        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<IEnumerable<Notification>> MarkAsReadAsync(string userId)
    {
        var unreadNotifications = await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToListAsync();

        foreach (var notification in unreadNotifications)
        {
            notification.IsRead = true;
        }

        await _context.SaveChangesAsync();
        return unreadNotifications;
    }
    
    public async Task DeleteNotification(int notificationId)
    {
        var notification = await _context.Notifications.FindAsync(notificationId);
        if (notification != null)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }
    }
    
    
    
    public async Task<IEnumerable<Notification>> GetAllNotificationsAsync(string userId)
    {
        return await _context.Notifications
            .Where(n => n.UserId == userId)
            .AsNoTracking()
            .OrderByDescending(n => n.CreatedOn)
            .ToListAsync();
    }
    
    public async Task<int> GetUnreadNotificationCountAsync(string userId)
    {
        return await _context.Notifications
            .CountAsync(n => n.UserId == userId && !n.IsRead);
    }
}