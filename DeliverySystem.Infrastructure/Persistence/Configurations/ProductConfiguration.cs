using DeliverySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.Description)
                   .HasMaxLength(500);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.Property(p => p.ImageUrl)
                   .HasMaxLength(300);

            //= العلاقة مع الفرع
            builder.HasOne(p => p.Branch)
                   .WithMany(b => b.Products)
                   .HasForeignKey(p => p.BranchId)
                   .OnDelete(DeleteBehavior.Restrict);

            //= العلاقة مع التصنيف
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}