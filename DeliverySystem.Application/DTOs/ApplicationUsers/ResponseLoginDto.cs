namespace DeliverySystem.Application.DTOs.ApplicationUsers;

public record ResponseLoginDto(
    string FullName,
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpire
);