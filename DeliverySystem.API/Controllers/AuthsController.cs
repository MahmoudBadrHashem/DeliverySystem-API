
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
    public async Task<IActionResult> RegisterUserAsync(RequestRegisterDto dto)
    {
        var response = await _authService.RegisterUserAsync(dto);
        string token = await _authService.GenerateTokenToConfirmEmail(dto.Email);
        string link = Url.Action(nameof(ConfirmEmail), "Auths", new { token, response.userId }, Request.Scheme)!;
        await _emailService.SendEmailAsync(dto.Email, link!, EmailType.ConfirmEmail);
        return Ok(response);
    }
    [HttpGet("Confirm")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string token, string userId)
    {
        await _authService.ConfirmEmailAsync(userId, token);
        return Ok();
    }
}
