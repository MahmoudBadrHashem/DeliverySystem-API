using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.Persistence.Configurations;

public class DeliveryAgentConfiguration : IEntityTypeConfiguration<DeliveryAgent>
{
    public void Configure(EntityTypeBuilder<DeliveryAgent> builder)
    {
        builder.HasKey(m => m.Id);
        builder.HasOne<ApplicationUser>()
        .WithOne()
        .HasForeignKey<DeliveryAgent>(e => e.UserId);

        builder.HasMany(da => da.Orders)
        .WithOne(o => o.DeliveryAgent)
        .HasForeignKey(o => o.DeliveryAgentId)
        .OnDelete(DeleteBehavior.Restrict)
        .IsRequired(false);
    }
}