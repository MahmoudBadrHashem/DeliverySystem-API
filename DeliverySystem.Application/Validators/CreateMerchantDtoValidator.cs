using FluentValidation;
using DeliverySystem.Application.DTOs.Merchants;

namespace DeliverySystem.Application.Validators
{
    public class CreateMerchantDtoValidator : AbstractValidator<CreateMerchantDto>
    {
        public CreateMerchantDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم التاجر مطلوب")
                .MaximumLength(150).WithMessage("اسم التاجر يجب ألا يتجاوز 150 حرفاً");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب");
        }
    }
}