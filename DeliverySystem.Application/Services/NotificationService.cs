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
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var notifications = await _unitOfWork.Notification.GetByUserIdAsync(userId, cancellationToken);
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

            await _unitOfWork.Notification.AddAsync(n, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return n.Id;
        }

        public async Task<bool> MarkAsReadAsync(int id, string userId, CancellationToken cancellationToken = default)
        {
            var n = await _unitOfWork.Notification.GetByIdAsync(id, cancellationToken);
            if (n == null || n.UserId != userId) return false;

            n.IsRead = true;
            await _unitOfWork.Notification.UpdateAsync(n, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(string userId, CancellationToken cancellationToken = default)
        {
            var notifications = await _unitOfWork.Notification.GetByUserIdAsync(userId, cancellationToken);
            var unread = notifications.Where(n => !n.IsRead).ToList();
            
            if (!unread.Any()) return true;

            foreach (var n in unread)
            {
                n.IsRead = true;
                await _unitOfWork.Notification.UpdateAsync(n, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteNotificationAsync(int id, string userId, CancellationToken cancellationToken = default)
        {
            var n = await _unitOfWork.Notification.GetByIdAsync(id, cancellationToken);
            if (n == null || n.UserId != userId) return false;

            await _unitOfWork.Notification.DeleteAsync(n, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<NotificationDto>> GetAllNotificationsAsync(CancellationToken cancellationToken = default)
        {
            var notifications = await _unitOfWork.Notification.GetAllAsync(cancellationToken);
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

        public async Task<bool> DeleteNotificationAsync(int id, CancellationToken cancellationToken = default)
        {
            var n = await _unitOfWork.Notification.GetByIdAsync(id, cancellationToken);
            if (n == null) return false;

            await _unitOfWork.Notification.DeleteAsync(n, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
