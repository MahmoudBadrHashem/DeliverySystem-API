using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeliverySystem.Application.DTOs.Addresses;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<IEnumerable<AddressDto>> GetAddressesByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var addresses = await _addressRepository.GetAllAsync();
            return addresses
                .Where(a => a.UserId == userId)
                .Select(a => new AddressDto
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    StreetName = a.StreetName,
                    BuildingNumber = a.BuildingNumber,
                    FloorNumber = a.FloorNumber,
                    ApartmentNumber = a.ApartmentNumber,
                    AdditionalDirections = a.AdditionalDirections,
                    Label = a.Label,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude
                })
                .ToList();
        }

        public async Task<AddressDto?> GetAddressByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var a = await _addressRepository.GetByIdAsync(id);
            if (a == null) return null;

            return new AddressDto
            {
                Id = a.Id,
                UserId = a.UserId,
                StreetName = a.StreetName,
                BuildingNumber = a.BuildingNumber,
                FloorNumber = a.FloorNumber,
                ApartmentNumber = a.ApartmentNumber,
                AdditionalDirections = a.AdditionalDirections,
                Label = a.Label,
                Latitude = a.Latitude,
                Longitude = a.Longitude
            };
        }

        public async Task<int> CreateAddressAsync(string userId, CreateAddressDto dto, CancellationToken cancellationToken = default)
        {
            var address = new Address
            {
                UserId = userId,
                StreetName = dto.StreetName,
                BuildingNumber = dto.BuildingNumber,
                FloorNumber = dto.FloorNumber,
                ApartmentNumber = dto.ApartmentNumber,
                AdditionalDirections = dto.AdditionalDirections,
                Label = dto.Label,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };

            await _addressRepository.AddAsync(address);
            return address.Id;
        }

        public async Task<bool> UpdateAddressAsync(int id, string userId, UpdateAddressDto dto, CancellationToken cancellationToken = default)
        {
            var a = await _addressRepository.GetByIdAsync(id);
            if (a == null || a.UserId != userId) return false;

            a.StreetName = dto.StreetName;
            a.BuildingNumber = dto.BuildingNumber;
            a.FloorNumber = dto.FloorNumber;
            a.ApartmentNumber = dto.ApartmentNumber;
            a.AdditionalDirections = dto.AdditionalDirections;
            a.Label = dto.Label;
            a.Latitude = dto.Latitude;
            a.Longitude = dto.Longitude;

            await _addressRepository.UpdateAsync(a);
            return true;
        }

        public async Task<bool> DeleteAddressAsync(int id, string userId, CancellationToken cancellationToken = default)
        {
            var a = await _addressRepository.GetByIdAsync(id);
            if (a == null || a.UserId != userId) return false;

            await _addressRepository.DeleteAsync(a);
            return true;
        }
    }
}