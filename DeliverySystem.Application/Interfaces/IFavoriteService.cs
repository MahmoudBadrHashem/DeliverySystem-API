using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Favorites;

namespace DeliverySystem.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<FavoriteDto>> GetCustomerFavoritesAsync(int customerId);
        Task<bool> AddToFavoritesAsync(CreateFavoriteDto dto);
        Task<bool> RemoveFromFavoritesAsync(int customerId, int productId);
    }
}