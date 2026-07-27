using System;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Front_Common;
using DeliverySystem.Application.DTOs.Merchants;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<PagedResponse<MerchantDto>> GetAllMerchantsAsync(string? search, int pageNumber = 1, int pageSize = 10)
        {
            var merchants = await _merchantRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                merchants = merchants.Where(m => m.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                              m.Email.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            int totalRecords = merchants.Count();

            var pagedData = merchants
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MerchantDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Email = m.Email,
                    Phone = m.Phone
                })
                .ToList();

            return new PagedResponse<MerchantDto>
            {
                Data = pagedData,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<MerchantDto?> GetMerchantByIdAsync(int id)
        {
            var m = await _merchantRepository.GetByIdAsync(id);
            if (m == null) return null;
            return new MerchantDto { Id = m.Id, Name = m.Name, Email = m.Email, Phone = m.Phone };
        }

        public async Task<int> CreateMerchantAsync(CreateMerchantDto dto)
        {
            var merchant = new Merchant { Name = dto.Name, Email = dto.Email, Phone = dto.Phone };
            await _merchantRepository.AddAsync(merchant);
            return merchant.Id;
        }

        public async Task<bool> UpdateMerchantAsync(int id, UpdateMerchantDto dto)
        {
            var existing = await _merchantRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            await _merchantRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteMerchantAsync(int id)
        {
            var existing = await _merchantRepository.GetByIdAsync(id);
            if (existing == null) return false;
            await _merchantRepository.DeleteAsync(existing);
            return true;
        }
    }
}