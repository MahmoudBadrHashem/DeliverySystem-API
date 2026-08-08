using Microsoft.EntityFrameworkCore;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Rating>> GetRatingsByMerchantAsync(int merchantId, CancellationToken cancellationToken = default) =>
            await _context.Ratings
                .Where(r => r.MerchantId == merchantId)
                .ToListAsync(cancellationToken);

        public async Task<IEnumerable<Rating>> GetRatingsByDeliveryAgentAsync(int deliveryAgentId, CancellationToken cancellationToken = default) =>
            await _context.Ratings
                .Where(r => r.DeliveryAgentId == deliveryAgentId)
                .ToListAsync(cancellationToken);
    }
}