using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        public BranchRepository(ApplicationDbContext context) : base(context)
        {
        }

        //= IDوظيفتها انها بتدخل تدور في الداتا وتجيب  كل فروع التاجر ده بناءعلى ال  
        public async Task<IEnumerable<Branch>> GetByMerchantIdAsync(int merchantId) =>
            await _context.Branches
                .Where(b => b.MerchantId == merchantId)
                .ToListAsync();
    }
}