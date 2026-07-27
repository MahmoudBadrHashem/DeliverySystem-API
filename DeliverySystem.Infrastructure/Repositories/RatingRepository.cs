using Microsoft.EntityFrameworkCore;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Rating>> GetRatingsByMerchantAsync(int merchantId) =>
            await _context.Ratings
                .Where(r => r.MerchantId == merchantId)
                .ToListAsync();

        public async Task<IEnumerable<Rating>> GetRatingsByDeliveryAgentAsync(int deliveryAgentId) =>
            await _context.Ratings
                .Where(r => r.DeliveryAgentId == deliveryAgentId)
                .ToListAsync();
    }
}