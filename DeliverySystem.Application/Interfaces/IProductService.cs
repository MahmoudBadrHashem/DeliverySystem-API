using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Front_Common;
using DeliverySystem.Application.DTOs.Products;

namespace DeliverySystem.Application.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponse<ProductDto>> GetAllProductsAsync(string? search, int? categoryId, int pageNumber = 1, int pageSize = 10);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<int> CreateProductAsync(CreateProductDto dto);
        Task<bool> UpdateProductAsync(int id, UpdateProductDto dto);
        Task<bool> DeleteProductAsync(int id);
    }
}