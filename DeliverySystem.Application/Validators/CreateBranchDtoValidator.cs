using FluentValidation;
using DeliverySystem.Application.DTOs.Branches;

namespace DeliverySystem.Application.Validators
{
    public class CreateBranchDtoValidator : AbstractValidator<CreateBranchDto>
    {
        public CreateBranchDtoValidator()
        {
            RuleFor(x => x.MerchantId)
                .GreaterThan(0).WithMessage("يجب اختيار تاجر صحيح");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم الفرع مطلوب")
                .MaximumLength(150).WithMessage("اسم الفرع يجب ألا يتجاوز 150 حرفاً");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("عنوان الفرع مطلوب")
                .MaximumLength(250).WithMessage("العنوان يجب ألا يتجاوز 250 حرفاً");
        }
    }
}