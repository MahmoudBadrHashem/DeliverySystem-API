using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private readonly IUnitOfWork _unitOfWork;

        public BranchService(IBranchRepository branchRepository, IUnitOfWork unitOfWork)
        {
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<BranchDto>> GetAllBranchesAsync(string? search, int? merchantId, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var branches = await _branchRepository.GetAllAsync(cancellationToken);

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
                    MerchantId = b.MerchantId,
                    IsActive = b.IsActive
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

        public async Task<BranchDto?> GetBranchByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var b = await _branchRepository.GetByIdAsync(id, cancellationToken);
            if (b == null) return null;
            return new BranchDto { Id = b.Id, Name = b.Name, Address = b.Address, MerchantId = b.MerchantId, IsActive = b.IsActive };
        }

        public async Task<IEnumerable<BranchDto>> GetBranchesByMerchantAsync(int merchantId, CancellationToken cancellationToken = default)
        {
            var branches = await _branchRepository.GetByMerchantIdAsync(merchantId, cancellationToken);
            return branches.Select(b => new BranchDto { Id = b.Id, Name = b.Name, Address = b.Address, MerchantId = b.MerchantId, IsActive = b.IsActive });
        }

        public async Task<int> CreateBranchAsync(CreateBranchDto dto, CancellationToken cancellationToken = default)
        {
            var branch = new Branch { Name = dto.Name, Address = dto.Address, MerchantId = dto.MerchantId, IsActive = true };
            await _branchRepository.AddAsync(branch, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return branch.Id;
        }

        public async Task<bool> UpdateBranchAsync(int id, UpdateBranchDto dto, CancellationToken cancellationToken = default)
        {
            var existing = await _branchRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Address = dto.Address;
            existing.MerchantId = dto.MerchantId;
            await _branchRepository.UpdateAsync(existing, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateBranchStatusAsync(int id, bool isActive, CancellationToken cancellationToken = default)
        {
            var existing = await _branchRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null) return false;

            existing.IsActive = isActive;
            await _branchRepository.UpdateAsync(existing, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteBranchAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _branchRepository.GetByIdAsync(id, cancellationToken);
            if (existing == null) return false;
            await _branchRepository.DeleteAsync(existing, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}