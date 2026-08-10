using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DeliverySystem.Application.DTOs.ApplicationUsers;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DeliverySystem.Infrastructure.JWTToken;

public class JwtTokenService : IJwtTokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtOptions _jwtOption;
    public JwtTokenService(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtOption, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _jwtOption = jwtOption.Value;
        _unitOfWork = unitOfWork;
    }

    public async Task<AccessToken> GenerateJwtTokenAsync(string userName, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            throw new Exception("User Not Found");

        var roles = await _userManager.GetRolesAsync(user);
        List<Claim> claims = [
            new Claim(ClaimTypes.Name,user.UserName!),
            new Claim(ClaimTypes.Email,user.Email!),
            new Claim(JwtRegisteredClaimNames.Sub,user.Id!),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        ];

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var securityToken = new JwtSecurityToken(
            issuer: _jwtOption.Issuer,
            audience: _jwtOption.Audience,
            signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Key)), SecurityAlgorithms.HmacSha256),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOption.ExpireMinutes)
        );

        return new AccessToken
        {
            Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
            Expiries = DateTime.UtcNow.AddMinutes(_jwtOption.ExpireMinutes)
        };
    }
    private string GenerateToken()
    {
        var token = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(token);
    }
    public async Task<RefreshToken> GenerateRefreshTokenAsync(string userName, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            throw new Exception("User is not Found");
        var getRefreshToken = await _unitOfWork.RefreshToken.GetFirstOneAsync(e =>
        e.UserId == user.Id && e.ExpiredOn >= DateTime.UtcNow && e.Revoked == null, cancellationToken);
        if (getRefreshToken == null)
        {
            var refreshToken = new RefreshToken
            {
                Token = GenerateToken(),
                ExpiredOn = DateTime.UtcNow.AddDays(14),
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id,
            };
            await _unitOfWork.RefreshToken.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();
            return refreshToken;
        }
        return getRefreshToken;
    }
}
