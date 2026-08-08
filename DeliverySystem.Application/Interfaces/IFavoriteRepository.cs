using DeliverySystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeliverySystem.Application.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>> GetCustomerFavoritesAsync(int customerId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int customerId, int productId, CancellationToken cancellationToken = default);
        Task AddAsync(Favorite favorite, CancellationToken cancellationToken = default);
        Task<bool> RemoveAsync(int customerId, int productId, CancellationToken cancellationToken = default);
    }
}