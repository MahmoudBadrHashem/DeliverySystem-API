using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Notifications;
using DeliverySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            return Ok(notifications);
        }

        [HttpPost("user/{userId}")]
        public async Task<IActionResult> Create(string userId, [FromBody] CreateNotificationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _notificationService.CreateNotificationAsync(userId, dto);
            return Ok(new { message = "تم إضافة الإشعار بنجاح", id });
        }

        [HttpPut("mark-read/{id}/user/{userId}")]
        public async Task<IActionResult> MarkAsRead(int id, string userId)
        {
            var result = await _notificationService.MarkAsReadAsync(id, userId);
            if (!result)
                return NotFound(new { message = "الإشعار غير موجود أو لا ينتمي لهذا المستخدم" });

            return Ok(new { message = "تم تحديد الإشعار كمقروء" });
        }

        [HttpPut("mark-all-read/user/{userId}")]
        public async Task<IActionResult> MarkAllAsRead(string userId)
        {
            await _notificationService.MarkAllAsReadAsync(userId);
            return Ok(new { message = "تم تحديد جميع الإشعارات كمقروءة" });
        }

        [HttpDelete("{id}/user/{userId}")]
        public async Task<IActionResult> Delete(int id, string userId)
        {
            var result = await _notificationService.DeleteNotificationAsync(id, userId);
            if (!result)
                return NotFound(new { message = "الإشعار غير موجود أو لا ينتمي لهذا المستخدم" });

            return Ok(new { message = "تم حذف الإشعار بنجاح" });
        }
    }
}
