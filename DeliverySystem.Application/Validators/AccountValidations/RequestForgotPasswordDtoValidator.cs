using DeliverySystem.Application.DTOs.ApplicationUsers;
using FluentValidation;

namespace DeliverySystem.Application.Validators.AccountValidations;

public class RequestForgotPasswordDtoValidator : AbstractValidator<RequestForgotPasswordDto>
{
    public RequestForgotPasswordDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
