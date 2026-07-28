using System;

namespace DeliverySystem.Application.DTOs.Coupons
{
    public class UpdateCouponDto
    {
        public string Code { get; set; } = string.Empty;
        public decimal DiscountAmount { get; set; }
        public bool IsPercentage { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? UsageLimit { get; set; }
        public bool IsActive { get; set; }
    }
}
