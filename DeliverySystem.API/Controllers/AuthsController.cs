
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DeliverySystem.Application.DTOs.ApplicationUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DeliverySystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IEmailService _emailService;

    public AuthsController(IAuthService authService, IEmailService emailService)
    {
        _authService = authService;
        _emailService = emailService;
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUserAsync(RequestRegisterDto dto, CancellationToken cancellationToken = default)
    {
        var response = await _authService.RegisterUserAsync(dto, cancellationToken);
        string token = await _authService.GenerateTokenToConfirmEmail(dto.Email, cancellationToken);
        string? link = Url.Action(nameof(ConfirmEmail), "Auths", new { token, response.userId }, Request.Scheme);
        if (string.IsNullOrEmpty(link))
            throw new InvalidOperationException("Failed to generate email confirmation link");
        await _emailService.SendEmailAsync(dto.Email, link, EmailType.ConfirmEmail, cancellationToken);
        return Ok(response);
    }
    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult<ResponseLoginDto>> Login(RequestLoginDto requestLoginDto, CancellationToken cancellationToken = default)
    {
        var response = await _authService.LoginAsync(requestLoginDto, cancellationToken);
        return Ok(response);
    }
    [Authorize]
    [HttpPost("LogOut")]
    public async Task<ActionResult> LogOut(string RefreshToken, CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            ?? throw new UnauthorizedAccessException("Invalid token ");
        await _authService.LogOutAsync(RefreshToken, userId, cancellationToken);

        return Ok(new
        {
            Success = true,
            StatusCode = 200,
            Message = "LogOut Successfully.."
        });
    }
    [Authorize]
    [HttpPost("RefreshToken")]
    public async Task<ActionResult> RefreshToken(string token, CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            ?? throw new UnauthorizedAccessException("Invalid token: missing subject claim");
        var response = await _authService.CreateRefreshTokenAsync(token, userId, cancellationToken);
        return Ok(response);
    }
    [HttpGet("Confirm")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string token, string userId)
    {
        await _authService.ConfirmEmailAsync(userId, token);
        return Ok();
    }
    [HttpPost("ForgotPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(RequestForgotPasswordDto dto, CancellationToken cancellationToken = default)
    {
        await _authService.ForgotPasswordAsync(dto.Email, cancellationToken);
        return Ok(new
        {
            Success = true,
            StatusCode = 200,
            Message = "If an account with that email exists, a password reset link has been sent."
        });
    }
    [HttpPost("ResetPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(RequestResetPasswordDto dto, CancellationToken cancellationToken = default)
    {
        await _authService.ResetPasswordAsync(dto, cancellationToken);
        return Ok(new
        {
            Success = true,
            StatusCode = 200,
            Message = "Password has been reset successfully."
        });
    }
    [Authorize]
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(RequestChangePasswordDto dto, CancellationToken cancellationToken = default)
    {
        var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
            ?? throw new UnauthorizedAccessException("Invalid token ");
        await _authService.ChangePasswordAsync(userId, dto, cancellationToken);
        return Ok(new
        {
            Success = true,
            StatusCode = 200,
            Message = "Password changed successfully."
        });
    }
}
