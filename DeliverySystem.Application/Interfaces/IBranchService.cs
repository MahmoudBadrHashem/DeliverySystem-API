using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Branches;
using DeliverySystem.Application.DTOs.Front_Common;

namespace DeliverySystem.Application.Interfaces
{
    public interface IBranchService
    {
        Task<PagedResponse<BranchDto>> GetAllBranchesAsync(string? search, int? merchantId, int pageNumber = 1, int pageSize = 10);
        Task<BranchDto?> GetBranchByIdAsync(int id);
        Task<IEnumerable<BranchDto>> GetBranchesByMerchantAsync(int merchantId);
        Task<int> CreateBranchAsync(CreateBranchDto dto);
        Task<bool> UpdateBranchAsync(int id, UpdateBranchDto dto);
        Task<bool> DeleteBranchAsync(int id);
    }
}