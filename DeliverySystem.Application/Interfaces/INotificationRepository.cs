using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Interfaces
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetByUserIdAsync(string userId);
    }
}
