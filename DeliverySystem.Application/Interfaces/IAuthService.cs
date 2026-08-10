using DeliverySystem.Application.DTOs.ApplicationUsers;

namespace DeliverySystem.Application.Interfaces;

public interface IAuthService

{
    Task<ResponseRefreshTokenDto> CreateRefreshTokenAsync(string? refreshToken, string userId, CancellationToken cancellationToken = default);
    Task<ResponseLoginDto> LoginAsync(RequestLoginDto dto, CancellationToken cancellationToken = default);
    Task<string> GenerateTokenToConfirmEmail(string email, CancellationToken cancellationToken = default);
    Task ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default);
    Task<ResponseRegisterDto> RegisterUserAsync(RequestRegisterDto requestRegisterDto, CancellationToken cancellationToken = default);
    Task LogOutAsync(string? refresh, string userId, CancellationToken cancellationToken = default);
    Task ForgotPasswordAsync(string email, CancellationToken cancellationToken = default);
    Task ResetPasswordAsync(RequestResetPasswordDto dto, CancellationToken cancellationToken = default);
    Task ChangePasswordAsync(string userId, RequestChangePasswordDto dto, CancellationToken cancellationToken = default);
}
