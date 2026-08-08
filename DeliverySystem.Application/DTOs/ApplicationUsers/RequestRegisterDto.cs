namespace DeliverySystem.Application.DTOs.ApplicationUsers;

public record RequestRegisterDto(

    string FullName,
    string UserName,
    string Email,
    string ConfirmEmail,
    string Password,
    string ConfirmPassword
);