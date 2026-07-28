using System.ComponentModel.DataAnnotations.Schema;

namespace DeliverySystem.Infrastructure.Persistence.Identity;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiredOn { get; set; }
    public bool IsExpired => DateTime.UtcNow > ExpiredOn;
    public DateTime? Revoked { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
    [ForeignKey("User")]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}