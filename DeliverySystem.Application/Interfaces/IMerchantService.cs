using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Front_Common;
using DeliverySystem.Application.DTOs.Merchants;

namespace DeliverySystem.Application.Interfaces
{
    public interface IMerchantService
    {
        Task<PagedResponse<MerchantDto>> GetAllMerchantsAsync(string? search, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        Task<MerchantDto?> GetMerchantByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CreateMerchantAsync(CreateMerchantDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateMerchantAsync(int id, UpdateMerchantDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteMerchantAsync(int id, CancellationToken cancellationToken = default);
    }
}