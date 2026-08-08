using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Coupons;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;

        public CouponService(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<IEnumerable<CouponDto>> GetAllCouponsAsync(CancellationToken cancellationToken = default)
        {
            var coupons = await _couponRepository.GetAllAsync(cancellationToken);
            return coupons.Select(c => new CouponDto
            {
                Id = c.Id,
                Code = c.Code,
                DiscountAmount = c.DiscountAmount,
                IsPercentage = c.IsPercentage,
                ExpiryDate = c.ExpiryDate,
                UsageLimit = c.UsageLimit,
                TimesUsed = c.TimesUsed,
                IsActive = c.IsActive
            }).ToList();
        }

        public async Task<CouponDto?> GetCouponByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var c = await _couponRepository.GetByIdAsync(id, cancellationToken);
            if (c == null) return null;

            return new CouponDto
            {
                Id = c.Id,
                Code = c.Code,
                DiscountAmount = c.DiscountAmount,
                IsPercentage = c.IsPercentage,
                ExpiryDate = c.ExpiryDate,
                UsageLimit = c.UsageLimit,
                TimesUsed = c.TimesUsed,
                IsActive = c.IsActive
            };
        }

        public async Task<CouponDto?> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            var c = await _couponRepository.GetByCodeAsync(code, cancellationToken);
            if (c == null) return null;

            return new CouponDto
            {
                Id = c.Id,
                Code = c.Code,
                DiscountAmount = c.DiscountAmount,
                IsPercentage = c.IsPercentage,
                ExpiryDate = c.ExpiryDate,
                UsageLimit = c.UsageLimit,
                TimesUsed = c.TimesUsed,
                IsActive = c.IsActive
            };
        }

        public async Task<int> CreateCouponAsync(CreateCouponDto dto, CancellationToken cancellationToken = default)
        {
            var coupon = new Coupon
            {
                Code = dto.Code.ToUpperInvariant(),
                DiscountAmount = dto.DiscountAmount,
                IsPercentage = dto.IsPercentage,
                ExpiryDate = dto.ExpiryDate,
                UsageLimit = dto.UsageLimit,
                TimesUsed = 0,
                IsActive = true
            };

            await _couponRepository.AddAsync(coupon, cancellationToken);
            return coupon.Id;
        }

        public async Task<bool> UpdateCouponAsync(int id, UpdateCouponDto dto, CancellationToken cancellationToken = default)
        {
            var c = await _couponRepository.GetByIdAsync(id, cancellationToken);
            if (c == null) return false;

            c.Code = dto.Code.ToUpperInvariant();
            c.DiscountAmount = dto.DiscountAmount;
            c.IsPercentage = dto.IsPercentage;
            c.ExpiryDate = dto.ExpiryDate;
            c.UsageLimit = dto.UsageLimit;
            c.IsActive = dto.IsActive;

            await _couponRepository.UpdateAsync(c, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteCouponAsync(int id, CancellationToken cancellationToken = default)
        {
            var c = await _couponRepository.GetByIdAsync(id, cancellationToken);
            if (c == null) return false;

            await _couponRepository.DeleteAsync(c, cancellationToken);
            return true;
        }

        public async Task<bool> ValidateCouponAsync(string code, CancellationToken cancellationToken = default)
        {
            var c = await _couponRepository.GetByCodeAsync(code, cancellationToken);
            if (c == null) return false;

            // Check if coupon is active, not expired, and usage limit is not exceeded
            if (!c.IsActive) return false;
            if (c.ExpiryDate < DateTime.UtcNow) return false;
            if (c.UsageLimit.HasValue && c.TimesUsed >= c.UsageLimit.Value) return false;

            return true;
        }
    }
}
