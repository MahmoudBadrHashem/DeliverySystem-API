using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.Persistence.Configurations
{
    public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<Merchant>(e => e.UserId);
        }
    }
}