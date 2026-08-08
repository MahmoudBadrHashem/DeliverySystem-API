using DeliverySystem.Application.DTOs.ApplicationUsers;
using FluentValidation;

namespace DeliverySystem.Application.Validators.AccountValidations;

public class RequestLoginDtoValidator : AbstractValidator<RequestLoginDto>
{
    public RequestLoginDtoValidator()
    {
        RuleFor(e => e.UserName)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("UserName is Required");

        RuleFor(e => e.Password)
        .Cascade(CascadeMode.Stop)
        .NotEmpty().WithMessage("Password is Required");
    }
}