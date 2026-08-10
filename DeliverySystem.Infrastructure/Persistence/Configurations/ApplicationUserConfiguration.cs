using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasMany(e => e.Orders)
        .WithOne()
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Favorites)
        .WithOne()
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Addresses)
        .WithOne()
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Ratings)
        .WithOne()
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Notifications)
        .WithOne()
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.CouponUsage)
        .WithOne()
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(e => e.RefreshTokens)
        .WithOne()
        .HasForeignKey(e => e.UserId)
        .OnDelete(DeleteBehavior.NoAction);
    }
}