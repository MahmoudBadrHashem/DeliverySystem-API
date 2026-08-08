using DeliverySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.Persistence.Configurations;

public class CouponUsageConfiguration : IEntityTypeConfiguration<CouponUsage>
{
    public void Configure(EntityTypeBuilder<CouponUsage> builder)
    {
        builder.HasKey(cu => cu.Id);

        builder.HasOne(cu => cu.Coupon)
            .WithMany()
            .HasForeignKey(cu => cu.CouponId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cu => cu.Order)
            .WithMany()
            .HasForeignKey(cu => cu.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
