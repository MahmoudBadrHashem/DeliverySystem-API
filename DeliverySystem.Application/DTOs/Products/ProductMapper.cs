using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.DTOs.Products
{
    public static class ProductMapper
    {
        public static Product ToEntity(this CreateProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };
        }

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }
    }
}