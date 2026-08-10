
namespace DeliverySystem.Application.DTOs.ApplicationUsers;

public record ResponseRefreshTokenDto(
    string RefreshToken,
    DateTime ExpiredRefreshToken,
    string AccessToken,
    DateTime ExpiredAccessToken
    );
