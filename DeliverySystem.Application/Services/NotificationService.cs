using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userId)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId);
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

        public async Task<int> CreateNotificationAsync(string userId, CreateNotificationDto dto)
        {
            var n = new Notification
            {
                UserId = userId,
                Title = dto.Title,
                Message = dto.Message,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _notificationRepository.AddAsync(n);
            return n.Id;
        }

        public async Task<bool> MarkAsReadAsync(int id, string userId)
        {
            var n = await _notificationRepository.GetByIdAsync(id);
            if (n == null || n.UserId != userId) return false;

            n.IsRead = true;
            await _notificationRepository.UpdateAsync(n);
            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(string userId)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(userId);
            var unread = notifications.Where(n => !n.IsRead).ToList();
            
            if (!unread.Any()) return true;

            foreach (var n in unread)
            {
                n.IsRead = true;
                await _notificationRepository.UpdateAsync(n);
            }

            return true;
        }

        public async Task<bool> DeleteNotificationAsync(int id, string userId)
        {
            var n = await _notificationRepository.GetByIdAsync(id);
            if (n == null || n.UserId != userId) return false;

            await _notificationRepository.DeleteAsync(n);
            return true;
        }
    }
}
