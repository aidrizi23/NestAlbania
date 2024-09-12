using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Data;
using NestAlbania.Services;

namespace NestAlbania.Controllers;
public class NotificationController : Controller
{
    private readonly INotificationService _notificationService;
    private readonly UserManager<ApplicationUser> _userManager;

    public NotificationController(INotificationService notificationService, UserManager<ApplicationUser> userManager)
    {
        _notificationService = notificationService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetNotifications()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var notifications = await _notificationService.GetAllNotificationsAsync(user.Id);
        return Json(notifications);
    }

    [HttpPost]
    public async Task<IActionResult> MarkAsRead()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        await _notificationService.MarkAsReadAsync(user.Id);
        return Ok();
    }

    private IActionResult Unauthorized()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        await _notificationService.DeleteNotification(id);
        return Ok();
    }
}