using DeliverySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliverySystem.Infrastructure.Persistence.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(f => f.Id);

            //= عشان العميل ما يضيفش نفس المنتج مرتين في المفضلة
            builder.HasIndex(f => new { f.CustomerId, f.ProductId }).IsUnique();


            //= المنتج الواحد ممكن يتضاف في مفضلة كذا حد ولو المنتج اتمسح ومبقاش موجود م التاجر مثلا المفضلة بتاعته بتطير معاه
            builder.HasOne(f => f.Product)
                   .WithMany(p => p.Favorites)
                   .HasForeignKey(f => f.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}