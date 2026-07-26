using DeliverySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.Persistence.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(b => b.Address)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(b => b.Latitude)
                   .HasColumnType("decimal(9,6)");

            builder.Property(b => b.Longitude)
                   .HasColumnType("decimal(9,6)");

            builder.HasOne(b => b.Merchant)
                   .WithMany(m => m.Branches)
                   .HasForeignKey(b => b.MerchantId)
                   .OnDelete(DeleteBehavior.Cascade); //=  كل تاجر ليه كذا فرع ولو التاجر اتمسح فروعه بتتمسح معاه تلقائي
        }
    }
}