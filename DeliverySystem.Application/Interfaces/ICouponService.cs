using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Coupons;

namespace DeliverySystem.Application.Interfaces
{
    public interface ICouponService
    {
        Task<IEnumerable<CouponDto>> GetAllCouponsAsync(CancellationToken cancellationToken = default);
        Task<CouponDto?> GetCouponByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<CouponDto?> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default);
        Task<int> CreateCouponAsync(CreateCouponDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateCouponAsync(int id, UpdateCouponDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteCouponAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ValidateCouponAsync(string code, CancellationToken cancellationToken = default);
    }
}
