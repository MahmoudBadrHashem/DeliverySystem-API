using DeliverySystem.Application.DTOs.ApplicationUsers;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Interfaces;

public interface IJwtTokenService
{
    Task<RefreshToken> GenerateRefreshTokenAsync(string userName, CancellationToken cancellationToken = default);
    Task<AccessToken> GenerateJwtTokenAsync(string userName, CancellationToken cancellationToken = default);
}