using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Favorites;

namespace DeliverySystem.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<FavoriteDto>> GetCustomerFavoritesAsync(int customerId, CancellationToken cancellationToken = default);
        Task<bool> AddToFavoritesAsync(CreateFavoriteDto dto, CancellationToken cancellationToken = default);
        Task<bool> RemoveFromFavoritesAsync(int customerId, int productId, CancellationToken cancellationToken = default);
    }
}