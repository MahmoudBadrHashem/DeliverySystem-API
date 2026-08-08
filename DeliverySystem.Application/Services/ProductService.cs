using System.Threading;
using DeliverySystem.Application.DTOs.Front_Common;
using DeliverySystem.Application.DTOs.Products;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResponse<ProductDto>> GetAllProductsAsync(string? search, int? categoryId, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);

            // 1. Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                              (p.Description != null && p.Description.Contains(search, StringComparison.OrdinalIgnoreCase)));
            }

            // 2. Filter
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            int totalRecords = products.Count(); // حساب العدد الإجمالي

            // 3. Pagination
            var pagedData = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    CategoryId = p.CategoryId
                })
                .ToList();

            return new PagedResponse<ProductDto>
            {
                Data = pagedData,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var p = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (p == null) return null;

            return new ProductDto { Id = p.Id, Name = p.Name, Price = p.Price, Description = p.Description, CategoryId = p.CategoryId };
        }

        public async Task<int> CreateProductAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
        {
            var product = new Product { Name = dto.Name, Price = dto.Price, Description = dto.Description, CategoryId = dto.CategoryId };
            await _productRepository.AddAsync(product, cancellationToken);
            return product.Id;
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductDto dto, CancellationToken cancellationToken = default)
        {
            var existing = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.Description = dto.Description;
            existing.CategoryId = dto.CategoryId;

            await _productRepository.UpdateAsync(existing, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null) return false;

            await _productRepository.DeleteAsync(existing, cancellationToken);
            return true;
        }
    }
}