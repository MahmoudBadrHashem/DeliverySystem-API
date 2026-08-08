using DeliverySystem.Application.DTOs.ApplicationUsers;
using DeliverySystem.Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace DeliverySystem.Application.services;

public class AuthService : IAuthService
{
    private readonly IValidator<RequestRegisterDto> _requestRegisterDtoValidator;
    private readonly IValidator<RequestLoginDto> _requestLoginDtoValidator;
    private readonly IDentityService _identityService;
    private readonly IEmailService _emailService;
    private readonly IJwtTokenService _jwtTokenService;
    public AuthService(IValidator<RequestRegisterDto> requestRegisterDtoValidator, IValidator<RequestLoginDto> requestLoginDtoValidator, IDentityService identityService, IEmailService emailService, IJwtTokenService jwtTokenService)
    {
        _requestRegisterDtoValidator = requestRegisterDtoValidator;
        _requestLoginDtoValidator = requestLoginDtoValidator;
        _identityService = identityService;
        _emailService = emailService;
        _jwtTokenService = jwtTokenService;
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
    public async Task LoginAsync(RequestLoginDto dto, CancellationToken cancellationToken = default)
    {
        var validatorResult = await _requestLoginDtoValidator.ValidateAsync(dto, cancellationToken);
        if (!validatorResult.IsValid)
            throw new ValidationException(validatorResult.Errors);
        var isExist = await _identityService.FindUserByUserName(dto.UserName, cancellationToken);
        if (!isExist)
            throw new ValidationException(new List<ValidationFailure> {
               new ValidationFailure("UserName","InValid UserName"),
               new ValidationFailure("Password","InValid Password")
            });
        var checkPassword = await _identityService.CheckPasswordAsync(dto.UserName, dto.Password, cancellationToken);
        if (!checkPassword)
            throw new ValidationException(new List<ValidationFailure> {
               new ValidationFailure("UserName","InValid UserName"),
               new ValidationFailure("Password","InValid Password")
            });


    }
    public async Task<string> GenerateTokenToConfirmEmail(string email, CancellationToken cancellationToken = default) => await _identityService.GenerateEmailConfirmationTokenAsync(email, cancellationToken);
    public async Task ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default)
    {
        var result = await _identityService.ConfirmEmailAsync(userId, token, cancellationToken);
        if (!result.IsSuccess)
            throw new ValidationException(result.Errors.Select(
                e => new ValidationFailure(e!.Code, e.Description)));
    }



}