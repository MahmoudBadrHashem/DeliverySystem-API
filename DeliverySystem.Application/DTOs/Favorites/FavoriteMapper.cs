using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.DTOs.Favorites
{
    public static class FavoriteMapper
    {
        public static Favorite ToEntity(this CreateFavoriteDto dto)
        {
            return new Favorite
            {
                CustomerId = dto.CustomerId,
                ProductId = dto.ProductId
            };
        }

        public static FavoriteDto ToDto(this Favorite favorite)
        {
            return new FavoriteDto
            {
                Id = favorite.Id,
                CustomerId = favorite.CustomerId,
                ProductId = favorite.ProductId
            };
        }
    }
}