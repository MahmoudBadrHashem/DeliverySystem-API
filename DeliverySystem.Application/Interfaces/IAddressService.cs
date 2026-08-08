using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Addresses;

namespace DeliverySystem.Application.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAddressesByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<AddressDto?> GetAddressByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CreateAddressAsync(string userId, CreateAddressDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateAddressAsync(int id, string userId, UpdateAddressDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAddressAsync(int id, string userId, CancellationToken cancellationToken = default);
    }
}
