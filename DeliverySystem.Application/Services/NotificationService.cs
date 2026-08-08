using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Notifications;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId, cancellationToken);
            return notifications.Select(n => new NotificationDto
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                CreatedAt = n.CreatedAt,
                IsRead = n.IsRead,
                UserId = n.UserId
            }).ToList();
        }

        public async Task<int> CreateNotificationAsync(string userId, CreateNotificationDto dto, CancellationToken cancellationToken = default)
        {
            var n = new Notification
            {
                UserId = userId,
                Title = dto.Title,
                Message = dto.Message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _notificationRepository.AddAsync(n, cancellationToken);
            return n.Id;
        }

        public async Task<bool> MarkAsReadAsync(int id, string userId, CancellationToken cancellationToken = default)
        {
            var n = await _notificationRepository.GetByIdAsync(id, cancellationToken);
            if (n == null || n.UserId != userId) return false;

            n.IsRead = true;
            await _notificationRepository.UpdateAsync(n, cancellationToken);
            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(string userId, CancellationToken cancellationToken = default)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId, cancellationToken);
            var unread = notifications.Where(n => !n.IsRead).ToList();
            
            if (!unread.Any()) return true;

            foreach (var n in unread)
            {
                n.IsRead = true;
                await _notificationRepository.UpdateAsync(n, cancellationToken);
            }

            return true;
        }

        public async Task<bool> DeleteNotificationAsync(int id, string userId, CancellationToken cancellationToken = default)
        {
            var n = await _notificationRepository.GetByIdAsync(id, cancellationToken);
            if (n == null || n.UserId != userId) return false;

            await _notificationRepository.DeleteAsync(n, cancellationToken);
            return true;
        }
    }
}
