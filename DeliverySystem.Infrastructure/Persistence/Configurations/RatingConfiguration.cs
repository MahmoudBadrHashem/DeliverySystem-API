using DeliverySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.Persistence.Configurations
{
       public class RatingConfiguration : IEntityTypeConfiguration<Rating>
       {
              public void Configure(EntityTypeBuilder<Rating> builder)
              {
                     builder.HasKey(r => r.Id);

                     builder.Property(r => r.Comment).HasMaxLength(500);

                     builder.HasOne(r => r.Order)
                            .WithMany(o => o.Ratings)
                            .HasForeignKey(r => r.OrderId)
                            .OnDelete(DeleteBehavior.Restrict);



                     builder.HasOne(r => r.Merchant)
                            .WithMany()
                            .HasForeignKey(r => r.MerchantId)
                            .OnDelete(DeleteBehavior.Restrict)
                            .IsRequired(false);

                     builder.HasOne(r => r.DeliveryAgent)
                            .WithMany()
                            .HasForeignKey(r => r.DeliveryAgentId)
                            .OnDelete(DeleteBehavior.Restrict)
                            .IsRequired(false);
              }
       }
}