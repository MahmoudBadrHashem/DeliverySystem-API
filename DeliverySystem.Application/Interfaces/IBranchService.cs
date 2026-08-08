using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Branches;
using DeliverySystem.Application.DTOs.Front_Common;

namespace DeliverySystem.Application.Interfaces
{
    public interface IBranchService
    {
        Task<PagedResponse<BranchDto>> GetAllBranchesAsync(string? search, int? merchantId, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        Task<BranchDto?> GetBranchByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BranchDto>> GetBranchesByMerchantAsync(int merchantId, CancellationToken cancellationToken = default);
        Task<int> CreateBranchAsync(CreateBranchDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateBranchAsync(int id, UpdateBranchDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteBranchAsync(int id, CancellationToken cancellationToken = default);
    }
}