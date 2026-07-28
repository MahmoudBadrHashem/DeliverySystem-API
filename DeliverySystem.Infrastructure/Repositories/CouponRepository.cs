using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Coupon?> GetByCodeAsync(string code)
        {
            return await _context.Coupons
                .FirstOrDefaultAsync(c => c.Code == code.ToUpperInvariant());
        }
    }
}
