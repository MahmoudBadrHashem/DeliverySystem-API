using System.ComponentModel.DataAnnotations.Schema;

namespace DeliverySystem.Domain.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiredOn { get; set; }
    public bool IsExpired => DateTime.UtcNow > ExpiredOn;
    public DateTime? Revoked { get; set; }
    public bool IsActive => Revoked == null && !IsExpired;
    [ForeignKey("User")]
    public string UserId { get; set; } = default!;
}