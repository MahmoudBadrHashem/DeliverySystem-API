using DeliverySystem.Application.DTOs.ApplicationUsers;

namespace DeliverySystem.Application.Interfaces;

public interface IAuthService
{
    Task<string> GenerateTokenToConfirmEmail(string email, CancellationToken cancellationToken = default);
    Task ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default);
    Task<ResponseRegisterDto> RegisterUserAsync(RequestRegisterDto requestRegisterDto, CancellationToken cancellationToken = default);
}
