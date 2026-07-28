using System;
using FluentValidation;
using DeliverySystem.Application.DTOs.Coupons;

namespace DeliverySystem.Application.Validators
{
    public class CreateCouponDtoValidator : AbstractValidator<CreateCouponDto>
    {
        public CreateCouponDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("كود الخصم مطلوب")
                .MaximumLength(50).WithMessage("كود الخصم يجب ألا يتجاوز 50 حرف");

            RuleFor(x => x.DiscountAmount)
                .GreaterThan(0).WithMessage("قيمة الخصم يجب أن تكون أكبر من صفر");

            RuleFor(x => x)
                .Custom((dto, context) =>
                {
                    if (dto.IsPercentage && dto.DiscountAmount > 100)
                    {
                        context.AddFailure(nameof(dto.DiscountAmount), "نسبة الخصم لا يمكن أن تتجاوز 100%");
                    }
                });

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("تاريخ انتهاء الصلاحية يجب أن يكون في المستقبل");
        }
    }
}
