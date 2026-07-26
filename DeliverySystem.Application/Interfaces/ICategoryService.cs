using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Categories;
using DeliverySystem.Application.DTOs.Front_Common;

namespace DeliverySystem.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<PagedResponse<CategoryDto>> GetAllCategoriesAsync(string? search, int pageNumber = 1, int pageSize = 10);
        Task<CategoryDto?> GetCategoryByIdAsync(int id);
        Task<int> CreateCategoryAsync(CreateCategoryDto dto);
        Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}