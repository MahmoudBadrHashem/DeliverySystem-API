using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Interfaces
{
    public interface ICouponRepository : IGenericRepository<Coupon>
    {
        Task<Coupon?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}
