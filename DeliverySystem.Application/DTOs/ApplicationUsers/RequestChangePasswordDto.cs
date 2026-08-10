namespace DeliverySystem.Application.DTOs.ApplicationUsers;

public record RequestChangePasswordDto(string CurrentPassword, string NewPassword, string ConfirmPassword);
