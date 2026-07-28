using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Notifications;

namespace DeliverySystem.Application.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userId);
        Task<int> CreateNotificationAsync(string userId, CreateNotificationDto dto);
        Task<bool> MarkAsReadAsync(int id, string userId);
        Task<bool> MarkAllAsReadAsync(string userId);
        Task<bool> DeleteNotificationAsync(int id, string userId);
    }
}
