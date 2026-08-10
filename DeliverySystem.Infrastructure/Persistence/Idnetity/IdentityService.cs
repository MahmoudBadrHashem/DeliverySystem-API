using DeliverySystem.Application.DTOs.ApplicationUsers;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace DeliverySystem.Infrastructure.Persistence.Identity;

public class IdentityService : IDentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

    }

    public async Task<Result> CreateUserAsync(RequestRegisterDto user, string password, CancellationToken cancellationToken = default)
    {
        var applicationUser = new ApplicationUser
        {
            UserName = user.UserName,
            FullName = user.FullName,
            Email = user.Email,
        };
        var result = await _userManager.CreateAsync(applicationUser, password);

        if (result.Succeeded)
        {
            return new Result(true);
        }

        return new Result(false, result.Errors.Select(e => new Errors(e.Code, e.Description)));
    }
    public async Task<string> GenerateEmailConfirmationTokenAsync(string email, CancellationToken cancellationToken = default)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email);
        if (applicationUser is null)
            throw new Exception("User Not Found ");
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
        return token;
    }
    public async Task<Result> ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default)
    {
        var applicationUser = await _userManager.FindByIdAsync(userId);
        if (applicationUser is null)
            throw new Exception("User Not Found ");
        var result = await _userManager.ConfirmEmailAsync(applicationUser, token);
        if (result.Succeeded)
        {
            return new Result(true);
        }
        return new Result(false, result.Errors.Select(e => new Errors(e.Code, e.Description)));
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string email, CancellationToken cancellationToken = default)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email);
        if (applicationUser is null)
            throw new Exception("User Not Found ");
        string token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
        return token;
    }
    public async Task<Result> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken = default)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email);
        if (applicationUser is null)
            throw new Exception("User Not Found ");
        var result = await _userManager.ResetPasswordAsync(applicationUser, token, newPassword);
        if (result.Succeeded)
        {
            return new Result(true);
        }
        return new Result(false, result.Errors.Select(e => new Errors(e.Code, e.Description)));
    }
    public async Task<Result> ChangePasswordAsync(string userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
    {
        var applicationUser = await _userManager.FindByIdAsync(userId);
        if (applicationUser is null)
            throw new Exception("User Not Found ");
        var result = await _userManager.ChangePasswordAsync(applicationUser, currentPassword, newPassword);
        if (result.Succeeded)
        {
            return new Result(true);
        }
        return new Result(false, result.Errors.Select(e => new Errors(e.Code, e.Description)));
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<string?> GetUserIdByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(userName);

        return user?.Id;
    }

    public async Task<string?> GetFullNameAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.FullName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return false;

        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> CheckPasswordAsync(
        string userName,
        string password,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
            return false;
        return await _userManager.CheckPasswordAsync(user, password);
    }
    public async Task<bool> FindUserByUserName(string userName, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            return false;
        return true;
    }
    public async Task<string?> GetUserIdByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw new Exception("user Is Not Found ");
        return user.Id;
    }

    public Task<string> GenerateTokenAsync(string userId, string role)
    {
        throw new NotImplementedException();
    }
}