using System.Collections.Generic;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Addresses;

namespace DeliverySystem.Application.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAddressesByUserIdAsync(string userId);
        Task<AddressDto?> GetAddressByIdAsync(int id);
        Task<int> CreateAddressAsync(string userId, CreateAddressDto dto);
        Task<bool> UpdateAddressAsync(int id, string userId, UpdateAddressDto dto);
        Task<bool> DeleteAddressAsync(int id, string userId);
    }
}
