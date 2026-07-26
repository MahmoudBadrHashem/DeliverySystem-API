using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Categories;
using DeliverySystem.Application.DTOs.Front_Common;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedResponse<CategoryDto>> GetAllCategoriesAsync(string? search, int pageNumber = 1, int pageSize = 10)
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                categories = categories.Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            int totalRecords = categories.Count();

            var pagedData = categories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();

            return new PagedResponse<CategoryDto>
            {
                Data = pagedData,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var c = await _categoryRepository.GetByIdAsync(id);
            if (c == null) return null;
            return new CategoryDto { Id = c.Id, Name = c.Name, Description = c.Description };
        }

        public async Task<int> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = new Category { Name = dto.Name, Description = dto.Description };
            await _categoryRepository.AddAsync(category);
            return category.Id;
        }

        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            await _categoryRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null) return false;
            await _categoryRepository.DeleteAsync(existing);
            return true;
        }
    }
}