using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Front_Common;
using DeliverySystem.Application.DTOs.Merchants;

namespace DeliverySystem.Application.Interfaces
{
    public interface IMerchantService
    {
        Task<PagedResponse<MerchantDto>> GetAllMerchantsAsync(string? search, int pageNumber = 1, int pageSize = 10);
        Task<MerchantDto?> GetMerchantByIdAsync(int id);
        Task<int> CreateMerchantAsync(CreateMerchantDto dto);
        Task<bool> UpdateMerchantAsync(int id, UpdateMerchantDto dto);
        Task<bool> DeleteMerchantAsync(int id);
    }
}