using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Coupons;

namespace DeliverySystem.Application.Interfaces
{
    public interface ICouponService
    {
        Task<IEnumerable<CouponDto>> GetAllCouponsAsync();
        Task<CouponDto?> GetCouponByIdAsync(int id);
        Task<CouponDto?> GetCouponByCodeAsync(string code);
        Task<int> CreateCouponAsync(CreateCouponDto dto);
        Task<bool> UpdateCouponAsync(int id, UpdateCouponDto dto);
        Task<bool> DeleteCouponAsync(int id);
        Task<bool> ValidateCouponAsync(string code);
    }
}
