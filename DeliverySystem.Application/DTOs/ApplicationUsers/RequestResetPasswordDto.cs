namespace DeliverySystem.Application.DTOs.ApplicationUsers;

public record RequestResetPasswordDto(string Email, string Token, string NewPassword, string ConfirmPassword);
