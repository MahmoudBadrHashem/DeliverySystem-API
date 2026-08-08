using DeliverySystem.Application.DTOs.ApplicationUsers;

namespace DeliverySystem.Application.Interfaces;

public interface IJwtTokenService
{
    Task<AccessToken> GenerateJwtTokenAsync(string userId, CancellationToken cancellationToken = default);
}