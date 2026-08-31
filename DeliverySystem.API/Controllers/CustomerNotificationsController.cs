using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/customer/notifications")]
    [Authorize]
    public class CustomerNotificationsController : BaseApiController
    {
        private readonly INotificationService _notificationService;

        public CustomerNotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyNotifications()
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(CurrentUserId);
            return Ok(notifications);
        }

        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var result = await _notificationService.MarkAsReadAsync(id, CurrentUserId);
            if (!result) return NotFound(new { message = "الإشعار غير موجود" });
            return Ok(new { message = "تم تحديد الإشعار كمقروء" });
        }

        [HttpPut("mark-all-read")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            await _notificationService.MarkAllAsReadAsync(CurrentUserId);
            return Ok(new { message = "تم تحديد جميع الإشعارات كمقروءة" });
        }
    }
}