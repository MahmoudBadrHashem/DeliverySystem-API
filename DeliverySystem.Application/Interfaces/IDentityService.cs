using DeliverySystem.Application.DTOs.ApplicationUsers;
using DeliverySystem.Domain.Common;

namespace DeliverySystem.Application.Interfaces
{
    public interface IDentityService
    {
        Task<string> GenerateEmailConfirmationTokenAsync(string email, CancellationToken cancellationToken = default);
        Task<Result> ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default);
        Task<Result> CreateUserAsync(RequestRegisterDto user, string password, CancellationToken cancellationToken = default);
        Task<string?> GetUserIdByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> FindUserByUserName(string userName, CancellationToken cancellationToken = default);
        Task<bool> CheckPasswordAsync(string userName, string password, CancellationToken cancellationToken = default);
    }
}