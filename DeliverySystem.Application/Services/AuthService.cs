using DeliverySystem.Application.DTOs.ApplicationUsers;
using DeliverySystem.Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace DeliverySystem.Application.services;

public class AuthService : IAuthService
{
    private readonly IValidator<RequestRegisterDto> _requestRegisterDtoValidator;
    private readonly IValidator<RequestLoginDto> _requestLoginDtoValidator;
    private readonly IValidator<RequestForgotPasswordDto> _requestForgotPasswordDtoValidator;
    private readonly IValidator<RequestResetPasswordDto> _requestResetPasswordDtoValidator;
    private readonly IValidator<RequestChangePasswordDto> _requestChangePasswordDtoValidator;
    private readonly IDentityService _identityService;
    private readonly IEmailService _emailService;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUnitOfWork _unitOfWork;
    public AuthService(IValidator<RequestRegisterDto> requestRegisterDtoValidator, IValidator<RequestLoginDto> requestLoginDtoValidator, IValidator<RequestForgotPasswordDto> requestForgotPasswordDtoValidator, IValidator<RequestResetPasswordDto> requestResetPasswordDtoValidator, IValidator<RequestChangePasswordDto> requestChangePasswordDtoValidator, IDentityService identityService, IEmailService emailService, IJwtTokenService jwtTokenService, IUnitOfWork unitOfWork)
    {
        _requestRegisterDtoValidator = requestRegisterDtoValidator;
        _requestLoginDtoValidator = requestLoginDtoValidator;
        _requestForgotPasswordDtoValidator = requestForgotPasswordDtoValidator;
        _requestResetPasswordDtoValidator = requestResetPasswordDtoValidator;
        _requestChangePasswordDtoValidator = requestChangePasswordDtoValidator;
        _identityService = identityService;
        _emailService = emailService;
        _jwtTokenService = jwtTokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterDto> RegisterUserAsync(RequestRegisterDto requestRegisterDto, CancellationToken cancellationToken = default)
    {
        var validationResult = await _requestRegisterDtoValidator.ValidateAsync(requestRegisterDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var result = await _identityService.CreateUserAsync(requestRegisterDto, requestRegisterDto.Password, cancellationToken);

        if (!result.IsSuccess)
        {
            throw new ValidationException(
                result.Errors.Select(
                e => new ValidationFailure(
                    e!.Code,
                    e!.Description)));
        }
        var userId = await _identityService.GetUserIdByEmailAsync(requestRegisterDto.Email, cancellationToken);
        return new ResponseRegisterDto(requestRegisterDto.UserName, userId!);
    }
    public async Task<ResponseLoginDto> LoginAsync(RequestLoginDto dto, CancellationToken cancellationToken = default)
    {
        var validatorResult = await _requestLoginDtoValidator.ValidateAsync(dto, cancellationToken);
        if (!validatorResult.IsValid)
            throw new ValidationException(validatorResult.Errors);
        var isExist = await _identityService.FindUserByUserName(dto.UserName, cancellationToken);
        if (!isExist)
            throw new ValidationException(new List<ValidationFailure> {
               new ValidationFailure("UserName","Invalid UserName"),
               new ValidationFailure("Password","Invalid Password ")
            });
        var checkPassword = await _identityService.CheckPasswordAsync(dto.UserName, dto.Password, cancellationToken);
        if (!checkPassword)
            throw new ValidationException(new List<ValidationFailure> {
          new ValidationFailure("UserName","Invalid UserName"),
               new ValidationFailure("Password","Invalid Password ")
            });

        var accessToken = await _jwtTokenService.GenerateJwtTokenAsync(dto.UserName, cancellationToken);
        var refreshToken = await _jwtTokenService.GenerateRefreshTokenAsync(dto.UserName, cancellationToken);
        var userId = await _identityService.GetUserIdByUserNameAsync(dto.UserName, cancellationToken);
        var fullName = await _identityService.GetFullNameAsync(userId!, cancellationToken);
        return new ResponseLoginDto
        (
             fullName ?? dto.UserName,
             accessToken.Token,
            refreshToken.Token,
            refreshToken.ExpiredOn
        );

    }
    public async Task LogOutAsync(string? refresh, string userId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(refresh))
            throw new Exception("InValid Token");
        var isExist = await _unitOfWork.RefreshToken.GetFirstOneAsync(e => e.Token == refresh && e.UserId == userId
        && e.IsActive, cancellationToken);
        if (isExist is null)
            throw new Exception("InValid Token");
        var refreshToken = await _unitOfWork.RefreshToken.GetAll(criteria: e => e.UserId == userId
        && e.IsActive, cancellationToken);
        foreach (var item in refreshToken)
            item.Revoked = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    public async Task<ResponseRefreshTokenDto> CreateRefreshTokenAsync(string? refreshToken, string userId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(refreshToken))
            throw new Exception("InValid Token");
        var getRefreshToken = await _unitOfWork.RefreshToken.GetFirstOneAsync(e => e.UserId == userId && e.Token == refreshToken
        && e.Revoked == null && e.ExpiredOn >= DateTime.UtcNow, cancellationToken);
        if (getRefreshToken == null)
            throw new Exception("InValid Token ");
        var userName = await _identityService.GetUserNameAsync(userId);
        if (string.IsNullOrEmpty(userName))
            throw new Exception("InValid Token ");
        getRefreshToken.Revoked = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        var accessToken = await _jwtTokenService.GenerateJwtTokenAsync(userName, cancellationToken);
        var newRefreshToken = await _jwtTokenService.GenerateRefreshTokenAsync(userName, cancellationToken);

        return new ResponseRefreshTokenDto(
            newRefreshToken.Token,
            newRefreshToken.ExpiredOn,
            accessToken.Token,
            accessToken.Expiries
        );

    }
    public async Task<string> GenerateTokenToConfirmEmail(string email, CancellationToken cancellationToken = default) => await _identityService.GenerateEmailConfirmationTokenAsync(email, cancellationToken);
    public async Task ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default)
    {
        var result = await _identityService.ConfirmEmailAsync(userId, token, cancellationToken);
        if (!result.IsSuccess)
            throw new ValidationException(result.Errors.Select(
                e => new ValidationFailure(e!.Code, e.Description)));
    }
    public async Task ForgotPasswordAsync(string email, CancellationToken cancellationToken = default)
    {
        var validationResult = await _requestForgotPasswordDtoValidator.ValidateAsync(new RequestForgotPasswordDto(email), cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var token = await _identityService.GeneratePasswordResetTokenAsync(email, cancellationToken);
        string link = $"api/Auths/ResetPassword?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";
        await _emailService.SendEmailAsync(email, link, EmailType.ResetPassword, cancellationToken);
    }
    public async Task ResetPasswordAsync(RequestResetPasswordDto dto, CancellationToken cancellationToken = default)
    {
        var validationResult = await _requestResetPasswordDtoValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var result = await _identityService.ResetPasswordAsync(dto.Email, dto.Token, dto.NewPassword, cancellationToken);
        if (!result.IsSuccess)
            throw new ValidationException(result.Errors.Select(
                e => new ValidationFailure(e!.Code, e.Description)));
    }
    public async Task ChangePasswordAsync(string userId, RequestChangePasswordDto dto, CancellationToken cancellationToken = default)
    {
        var validationResult = await _requestChangePasswordDtoValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var result = await _identityService.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword, cancellationToken);
        if (!result.IsSuccess)
            throw new ValidationException(result.Errors.Select(
                e => new ValidationFailure(e!.Code, e.Description)));
    }

}