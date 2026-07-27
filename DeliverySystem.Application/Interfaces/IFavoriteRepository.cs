using DeliverySystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySystem.Application.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>> GetCustomerFavoritesAsync(int customerId);
        Task<bool> ExistsAsync(int customerId, int productId);
        Task AddAsync(Favorite favorite);
        Task<bool> RemoveAsync(int customerId, int productId);
    }
}