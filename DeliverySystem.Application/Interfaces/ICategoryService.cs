using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Categories;
using DeliverySystem.Application.DTOs.Front_Common;

namespace DeliverySystem.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<PagedResponse<CategoryDto>> GetAllCategoriesAsync(string? search, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        Task<CategoryDto?> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CreateCategoryAsync(CreateCategoryDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteCategoryAsync(int id, CancellationToken cancellationToken = default);
    }
}