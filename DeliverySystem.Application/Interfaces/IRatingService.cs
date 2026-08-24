using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Ratings;

namespace DeliverySystem.Application.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingDto>> GetAllRatingsAsync(CancellationToken cancellationToken = default);
        Task<RatingDto?> GetRatingByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<RatingDto>> GetRatingsByOrderIdAsync(int orderId, CancellationToken cancellationToken = default);
        Task<int> CreateRatingAsync(CreateRatingDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteRatingAsync(int id, CancellationToken cancellationToken = default);
    }
}