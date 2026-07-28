using DeliverySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.StreetName)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(a => a.BuildingNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(a => a.FloorNumber)
                   .HasMaxLength(50);

            builder.Property(a => a.ApartmentNumber)
                   .HasMaxLength(50);

            builder.Property(a => a.AdditionalDirections)
                   .HasMaxLength(500);

            builder.Property(a => a.Label)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(a => a.Latitude)
                   .HasColumnType("decimal(9,6)");

            builder.Property(a => a.Longitude)
                   .HasColumnType("decimal(9,6)");

            builder.Property(a => a.UserId)
                   .IsRequired();
        }
    }
}
