using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Favorite> Favorites => Set<Favorite>();
        public DbSet<Merchant> Merchants => Set<Merchant>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<DeliveryAgent> DeliveryAgents => Set<DeliveryAgent>();
        public DbSet<Coupon> Coupons => Set<Coupon>();
        public DbSet<Address> Addresses => Set<Address>();
<<<<<<< HEAD
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
=======
        public DbSet<Notification> Notifications => Set<Notification>();
>>>>>>> origin/main

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}