using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Front_Common;
using DeliverySystem.Application.DTOs.Products;

namespace DeliverySystem.Application.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponse<ProductDto>> GetAllProductsAsync(string? search, int? categoryId, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        Task<ProductDto?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CreateProductAsync(CreateProductDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateProductAsync(int id, UpdateProductDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken = default);
    }
}