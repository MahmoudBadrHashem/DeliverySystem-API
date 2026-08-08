namespace DeliverySystem.Application.DTOs.ApplicationUsers;

public class AccessToken
{
    public string Token { get; set; } = default!;
    public DateTime Expiries { get; set; }
}