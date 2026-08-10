using DeliverySystem.Application.DTOs.Ratings;

namespace DeliverySystem.Application.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingDto>> GetAllRatingsAsync();
        Task<RatingDto?> GetRatingByIdAsync(int id);
        Task<int> CreateRatingAsync(CreateRatingDto dto);
        Task<bool> DeleteRatingAsync(int id);
    }
}