using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Notifications;

namespace DeliverySystem.Application.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<int> CreateNotificationAsync(string userId, CreateNotificationDto dto, CancellationToken cancellationToken = default);
        Task<bool> MarkAsReadAsync(int id, string userId, CancellationToken cancellationToken = default);
        Task<bool> MarkAllAsReadAsync(string userId, CancellationToken cancellationToken = default);
        Task<bool> DeleteNotificationAsync(int id, string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync(CancellationToken cancellationToken = default);
        Task<bool> DeleteNotificationAsync(int id, CancellationToken cancellationToken = default);
    }
}
