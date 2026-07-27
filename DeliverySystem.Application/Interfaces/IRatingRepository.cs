using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Interfaces
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<IEnumerable<Rating>> GetRatingsByMerchantAsync(int merchantId);
        Task<IEnumerable<Rating>> GetRatingsByDeliveryAgentAsync(int deliveryAgentId);
    }
}