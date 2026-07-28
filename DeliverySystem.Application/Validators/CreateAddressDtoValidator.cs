using FluentValidation;
using DeliverySystem.Application.DTOs.Addresses;

namespace DeliverySystem.Application.Validators
{
    public class CreateAddressDtoValidator : AbstractValidator<CreateAddressDto>
    {
        public CreateAddressDtoValidator()
        {
            RuleFor(x => x.StreetName)
                .NotEmpty().WithMessage("اسم الشارع مطلوب")
                .MaximumLength(250).WithMessage("اسم الشارع يجب ألا يتجاوز 250 حرف");

            RuleFor(x => x.BuildingNumber)
                .NotEmpty().WithMessage("رقم المبنى مطلوب")
                .MaximumLength(50).WithMessage("رقم المبنى يجب ألا يتجاوز 50 حرف");

            RuleFor(x => x.Label)
                .NotEmpty().WithMessage("نوع العنوان (مثال: المنزل، العمل) مطلوب")
                .MaximumLength(50).WithMessage("نوع العنوان يجب ألا يتجاوز 50 حرف");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("خط العرض الجغرافي غير صحيح");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("خط الطول الجغرافي غير صحيح");
        }
    }
}
