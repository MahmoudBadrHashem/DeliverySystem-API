using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Branches;
using DeliverySystem.Application.DTOs.Front_Common;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }


         public async Task<PagedResponse<BranchDto>> GetAllBranchesAsync(string? search, int? merchantId, int pageNumber = 1, int pageSize = 10)
        {
            var branches = await _branchRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                branches = branches.Where(b => b.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                              b.Address.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (merchantId.HasValue)
            {
                branches = branches.Where(b => b.MerchantId == merchantId.Value);
            }

            int totalRecords = branches.Count();

            var pagedData = branches
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BranchDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Address = b.Address,
                    MerchantId = b.MerchantId
                })
                .ToList();

            return new PagedResponse<BranchDto>
            {
                Data = pagedData,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }
        //=== id الحصول على فرع  
        public async Task<BranchDto?> GetBranchByIdAsync(int id)
        {
            var b = await _branchRepository.GetByIdAsync(id);
            if (b == null) return null;
            return new BranchDto { Id = b.Id, Name = b.Name, Address = b.Address, MerchantId = b.MerchantId };
        }
        //=== الحصول على الفروع حسب التاجر
        public async Task<IEnumerable<BranchDto>> GetBranchesByMerchantAsync(int merchantId)
        {
            var branches = await _branchRepository.GetByMerchantIdAsync(merchantId);
            return branches.Select(b => new BranchDto { Id = b.Id, Name = b.Name, Address = b.Address, MerchantId = b.MerchantId });
        }
        //=== إنشاء فرع جديد
        public async Task<int> CreateBranchAsync(CreateBranchDto dto)
        {
            var branch = new Branch { Name = dto.Name, Address = dto.Address, MerchantId = dto.MerchantId };
            await _branchRepository.AddAsync(branch);
            return branch.Id;
        }
        //==== تحديث فرع 
        public async Task<bool> UpdateBranchAsync(int id, UpdateBranchDto dto)
        {
            var existing = await _branchRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Address = dto.Address;
            existing.MerchantId = dto.MerchantId;
            await _branchRepository.UpdateAsync(existing);
            return true;
        }
        //==== تحديث فرع موجود   
        public async Task<bool> UpdateBranchStatusAsync(int id, bool isActive)
        {
            var existing = await _branchRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.IsActive = isActive;
            await _branchRepository.UpdateAsync(existing);
            return true;
        }
        //==== حذف فرع 
        public async Task<bool> DeleteBranchAsync(int id)
        {
            var existing = await _branchRepository.GetByIdAsync(id);
            if (existing == null) return false;
            await _branchRepository.DeleteAsync(existing);
            return true;
        }
    }
}