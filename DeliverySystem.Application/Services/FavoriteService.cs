using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Favorites;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<IEnumerable<FavoriteDto>> GetCustomerFavoritesAsync(int customerId)
        {
            var favorites = await _favoriteRepository.GetCustomerFavoritesAsync(customerId);

            return favorites.Select(f => new FavoriteDto
            {
                Id = f.Id,
                CustomerId = f.CustomerId,
                ProductId = f.ProductId,
                ProductName = f.Product != null ? f.Product.Name : string.Empty,
                ProductPrice = f.Product != null ? f.Product.Price : 0,
                ProductImageUrl = f.Product != null ? f.Product.ImageUrl : string.Empty
            });
        }

        public async Task<bool> AddToFavoritesAsync(CreateFavoriteDto dto)
        {
            var exists = await _favoriteRepository.ExistsAsync(dto.CustomerId, dto.ProductId);
            if (exists) return false;

            var favorite = new Favorite
            {
                CustomerId = dto.CustomerId,
                ProductId = dto.ProductId,
                CreatedDate = System.DateTime.UtcNow
            };

            await _favoriteRepository.AddAsync(favorite);
            return true;
        }

        public async Task<bool> RemoveFromFavoritesAsync(int customerId, int productId)
        {
            return await _favoriteRepository.RemoveAsync(customerId, productId);
        }
    }
}