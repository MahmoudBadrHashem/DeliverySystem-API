using DeliverySystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeliverySystem.Application.Interfaces
{
    public interface IBranchRepository : IGenericRepository<Branch>
    {
        Task<IEnumerable<Branch>> GetByMerchantIdAsync(int merchantId, CancellationToken cancellationToken = default);
    }
}