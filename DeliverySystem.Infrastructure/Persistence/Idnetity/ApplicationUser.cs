
using DeliverySystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DeliverySystem.Infrastructure.Persistence.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = null!;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<CouponUsage> CouponUsage { get; set; } = new List<CouponUsage>();

    public ICollection<Address> Addresses { get; set; } = new List<Address>();

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}