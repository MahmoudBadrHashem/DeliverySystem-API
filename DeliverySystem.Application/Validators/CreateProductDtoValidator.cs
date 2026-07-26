using FluentValidation;
using DeliverySystem.Application.DTOs.Products;

namespace DeliverySystem.Application.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.BranchId)
                .GreaterThan(0).WithMessage("يجب اختيار فرع صحيح");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("يجب اختيار تصنيف صحيح");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم المنتج مطلوب")
                .MaximumLength(150).WithMessage("اسم المنتج يجب ألا يتجاوز 150 حرفاً");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("وصف المنتج مطلوب")
                .MaximumLength(500).WithMessage("الوصف يجب ألا يتجاوز 500 حرفاً");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("السعر يجب أن يكون أكبر من الصفر");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("كمية المخزن لا يمكن أن تكون سالبة");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("رابط الصورة مطلوب");
        }
    }
}