using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace DeliverySystem.Domain.Entities;

public class CouponUsage
{
    public int Id { get; set; }
    //foreign key Application user
    public string UserId { get; set; } = default!;
    [ForeignKey($"{nameof(Coupon)}")]
    public int CouponId { get; set; }
    public Coupon Coupon { get; set; } = default!;
    public DateTime UsedAt { get; set; }
    [ForeignKey($"{nameof(Order)}")]
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;
}