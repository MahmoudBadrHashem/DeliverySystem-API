using DeliverySystem.Application.DTOs.ApplicationUsers;
using DeliverySystem.Domain.Common;

namespace DeliverySystem.Application.Interfaces
{
    public interface IDentityService
    {
        Task<string?> GetUserIdByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<string?> GetUserIdByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        Task<string?> GetUserNameAsync(string userId);
        Task<string?> GetFullNameAsync(string userId, CancellationToken cancellationToken = default);
        Task<string> GenerateEmailConfirmationTokenAsync(string email, CancellationToken cancellationToken = default);
        Task<string> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken = default);
        Task<Result> ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default);
        Task<Result> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken = default);
        Task<Result> ChangePasswordAsync(string userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default);
        Task<Result> CreateUserAsync(RequestRegisterDto user, string password, CancellationToken cancellationToken = default);
        Task<bool> FindUserByUserName(string userName, CancellationToken cancellationToken = default);
        Task<bool> CheckPasswordAsync(string userName, string password, CancellationToken cancellationToken = default);
    }
}