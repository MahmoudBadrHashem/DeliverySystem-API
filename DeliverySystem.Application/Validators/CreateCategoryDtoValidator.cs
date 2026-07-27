using FluentValidation;
using DeliverySystem.Application.DTOs.Categories;

namespace DeliverySystem.Application.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم التصنيف مطلوب")
                .MaximumLength(100).WithMessage("اسم التصنيف يجب ألا يتجاوز 100 حرف");
        }
    }
}