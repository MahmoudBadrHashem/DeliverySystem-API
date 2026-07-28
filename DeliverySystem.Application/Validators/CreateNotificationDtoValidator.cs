using FluentValidation;
using DeliverySystem.Application.DTOs.Notifications;

namespace DeliverySystem.Application.Validators
{
    public class CreateNotificationDtoValidator : AbstractValidator<CreateNotificationDto>
    {
        public CreateNotificationDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("عنوان الإشعار مطلوب")
                .MaximumLength(150).WithMessage("عنوان الإشعار يجب ألا يتجاوز 150 حرف");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("محتوى الإشعار مطلوب")
                .MaximumLength(1000).WithMessage("محتوى الإشعار يجب ألا يتجاوز 1000 حرف");
        }
    }
}
