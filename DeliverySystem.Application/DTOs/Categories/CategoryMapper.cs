using DeliverySystem.Application.DTOs.Categories;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.DTOs.Categories
{
    public static class CategoryMapper
    {
        //==  DTO لـ Entity عشان نحفظ في الداتا
        public static Category ToEntity(this CreateCategoryDto dto)
        {
            return new Category
            {
                Name = dto.Name
            };
        }

        //== Entity لـ DTO عشان نرجع البيانات للعميل
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}