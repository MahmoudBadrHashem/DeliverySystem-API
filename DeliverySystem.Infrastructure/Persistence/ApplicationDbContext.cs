using DeliverySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //= جدول التصنيفات
        public DbSet<Category> Categories => Set<Category>();

        //= جدول المنتجات
        public DbSet<Product> Products => Set<Product>();

        //= جدول الفروع
        public DbSet<Branch> Branches => Set<Branch>();

        //= جدول المفضلة
        public DbSet<Favorite> Favorites => Set<Favorite>();

        //= جدول التجار
        public DbSet<Merchant> Merchants => Set<Merchant>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<DeliveryAgent> DeliveryAgents => Set<DeliveryAgent>();
        public DbSet<Coupon> Coupons => Set<Coupon>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Notification> Notifications => Set<Notification>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //= السطر ده بقا اللي  بيجمع كل اعدادات الجداول اللي عملناها بره زي الشروط والطول والمفاتيح والعلاقات 
            // ويربطهم ببعض اوتوماتيك ويحطهم في الداتا بيس . بدل ما نقعد نكتب كل اعدادات الجداول دي هنا ونبوظ شكل الكود
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}