using DeliverySystem.Application.DTOs.ApplicationUsers;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Application.Services;
using DeliverySystem.Application.services;
using DeliverySystem.Domain.Common;
using DeliverySystem.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace DeliverySystem.Application.Tests;

public class AuthServiceTests
{
    private readonly Mock<IValidator<RequestRegisterDto>> _registerValidator;
    private readonly Mock<IValidator<RequestLoginDto>> _loginValidator;
    private readonly Mock<IValidator<RequestForgotPasswordDto>> _forgotValidator;
    private readonly Mock<IValidator<RequestResetPasswordDto>> _resetValidator;
    private readonly Mock<IValidator<RequestChangePasswordDto>> _changeValidator;
    private readonly Mock<IDentityService> _identityService;
    private readonly Mock<IEmailService> _emailService;
    private readonly Mock<IJwtTokenService> _jwtTokenService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly AuthService _sut;

    public AuthServiceTests()
    {
        _registerValidator = new Mock<IValidator<RequestRegisterDto>>();
        _loginValidator = new Mock<IValidator<RequestLoginDto>>();
        _forgotValidator = new Mock<IValidator<RequestForgotPasswordDto>>();
        _resetValidator = new Mock<IValidator<RequestResetPasswordDto>>();
        _changeValidator = new Mock<IValidator<RequestChangePasswordDto>>();
        _identityService = new Mock<IDentityService>();
        _emailService = new Mock<IEmailService>();
        _jwtTokenService = new Mock<IJwtTokenService>();
        _unitOfWork = new Mock<IUnitOfWork>();

        _sut = new AuthService(
            _registerValidator.Object,
            _loginValidator.Object,
            _forgotValidator.Object,
            _resetValidator.Object,
            _changeValidator.Object,
            _identityService.Object,
            _emailService.Object,
            _jwtTokenService.Object,
            _unitOfWork.Object);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldReturnResponse_WhenRegistrationSucceeds()
    {
        var dto = new RequestRegisterDto("Full Name", "testuser", "test@example.com", "test@example.com", "Password123", "Password123");
        _registerValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        _identityService.Setup(s => s.CreateUserAsync(dto, dto.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result(true));
        _identityService.Setup(s => s.GetUserIdByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync("user-123");

        var result = await _sut.RegisterUserAsync(dto);

        result.Should().NotBeNull();
        result.UserName.Should().Be("testuser");
        result.userId.Should().Be("user-123");
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldThrowValidationException_WhenDtoIsInvalid()
    {
        var dto = new RequestRegisterDto("", "", "", "", "", "");
        _registerValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("UserName", "Required") }));

        var act = async () => await _sut.RegisterUserAsync(dto);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnLoginResponse_WhenCredentialsAreValid()
    {
        var dto = new RequestLoginDto("testuser", "Password123");
        _loginValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        _identityService.Setup(s => s.FindUserByUserName(dto.UserName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _identityService.Setup(s => s.CheckPasswordAsync(dto.UserName, dto.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _identityService.Setup(s => s.GetUserIdByUserNameAsync(dto.UserName, It.IsAny<CancellationToken>()))
            .ReturnsAsync("user-123");
        _identityService.Setup(s => s.GetFullNameAsync("user-123", It.IsAny<CancellationToken>()))
            .ReturnsAsync("Test User");
        _jwtTokenService.Setup(s => s.GenerateJwtTokenAsync(dto.UserName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AccessToken { Token = "access-token", Expiries = DateTime.UtcNow.AddMinutes(30) });
        _jwtTokenService.Setup(s => s.GenerateRefreshTokenAsync(dto.UserName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RefreshToken { Token = "refresh-token", ExpiredOn = DateTime.UtcNow.AddDays(14) });

        var result = await _sut.LoginAsync(dto);

        result.Should().NotBeNull();
        result.AccessToken.Should().Be("access-token");
        result.RefreshToken.Should().Be("refresh-token");
        result.FullName.Should().Be("Test User");
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowValidationException_WhenUserDoesNotExist()
    {
        var dto = new RequestLoginDto("nonexistent", "Password123");
        _loginValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        _identityService.Setup(s => s.FindUserByUserName(dto.UserName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = async () => await _sut.LoginAsync(dto);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowValidationException_WhenPasswordIsInvalid()
    {
        var dto = new RequestLoginDto("testuser", "WrongPassword");
        _loginValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        _identityService.Setup(s => s.FindUserByUserName(dto.UserName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _identityService.Setup(s => s.CheckPasswordAsync(dto.UserName, dto.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = async () => await _sut.LoginAsync(dto);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ForgotPasswordAsync_ShouldSendEmail_WhenEmailIsValid()
    {
        var email = "test@example.com";
        _forgotValidator.Setup(v => v.ValidateAsync(It.IsAny<RequestForgotPasswordDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        _identityService.Setup(s => s.GeneratePasswordResetTokenAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync("reset-token");

        await _sut.ForgotPasswordAsync(email);

        _emailService.Verify(e => e.SendEmailAsync(email, It.IsAny<string>(), EmailType.ResetPassword, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ForgotPasswordAsync_ShouldThrowValidationException_WhenEmailIsInvalid()
    {
        var email = "invalid-email";
        _forgotValidator.Setup(v => v.ValidateAsync(It.IsAny<RequestForgotPasswordDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("Email", "Invalid email format.") }));

        var act = async () => await _sut.ForgotPasswordAsync(email);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ResetPasswordAsync_ShouldSucceed_WhenTokenIsValid()
    {
        var dto = new RequestResetPasswordDto("test@example.com", "reset-token", "NewPass123", "NewPass123");
        _resetValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        _identityService.Setup(s => s.ResetPasswordAsync(dto.Email, dto.Token, dto.NewPassword, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result(true));

        var act = async () => await _sut.ResetPasswordAsync(dto);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ResetPasswordAsync_ShouldThrowValidationException_WhenPasswordsDoNotMatch()
    {
        var dto = new RequestResetPasswordDto("test@example.com", "reset-token", "NewPass123", "DifferentPass");
        _resetValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("ConfirmPassword", "Passwords do not match.") }));

        var act = async () => await _sut.ResetPasswordAsync(dto);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ChangePasswordAsync_ShouldSucceed_WhenCurrentPasswordIsValid()
    {
        var userId = "user-123";
        var dto = new RequestChangePasswordDto("CurrentPass123", "NewPass123", "NewPass123");
        _changeValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        _identityService.Setup(s => s.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result(true));

        var act = async () => await _sut.ChangePasswordAsync(userId, dto);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ChangePasswordAsync_ShouldThrowValidationException_WhenCurrentPasswordIsEmpty()
    {
        var userId = "user-123";
        var dto = new RequestChangePasswordDto("", "NewPass123", "NewPass123");
        _changeValidator.Setup(v => v.ValidateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("CurrentPassword", "Current password is required.") }));

        var act = async () => await _sut.ChangePasswordAsync(userId, dto);

        await act.Should().ThrowAsync<ValidationException>();
    }
}
