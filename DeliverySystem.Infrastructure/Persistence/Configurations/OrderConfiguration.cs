using DeliverySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.Persistence.Configurations
{
       public class OrderConfiguration : IEntityTypeConfiguration<Order>
       {
              public void Configure(EntityTypeBuilder<Order> builder)
              {
                     builder.HasKey(o => o.Id);

                     builder.Property(o => o.TotalAmount).HasColumnType("decimal(10,2)");
                     builder.Property(o => o.DiscountAmount).HasColumnType("decimal(10,2)");

                     builder.HasOne(o => o.Branch)
                            .WithMany()
                            .HasForeignKey(o => o.BranchId)
                            .OnDelete(DeleteBehavior.Restrict);



                     builder.HasOne(o => o.Address)
                            .WithMany()
                            .HasForeignKey(o => o.AddressId)
                            .OnDelete(DeleteBehavior.Restrict);

                     builder.HasOne(o => o.Coupon)
                            .WithMany()
                            .HasForeignKey(o => o.CouponId)
                            .OnDelete(DeleteBehavior.Restrict)
                            .IsRequired(false);
              }
       }
}