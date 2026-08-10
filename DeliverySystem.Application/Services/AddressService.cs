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
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AddressDto>> GetAddressesByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var addresses = await _unitOfWork.Address.GetAllAsync(cancellationToken);
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
            var a = await _unitOfWork.Address.GetByIdAsync(id, cancellationToken);
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

            await _unitOfWork.Address.AddAsync(address, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return address.Id;
        }

        public async Task<bool> UpdateAddressAsync(int id, string userId, UpdateAddressDto dto, CancellationToken cancellationToken = default)
        {
            var a = await _unitOfWork.Address.GetByIdAsync(id, cancellationToken);
            if (a == null || a.UserId != userId) return false;

            a.StreetName = dto.StreetName;
            a.BuildingNumber = dto.BuildingNumber;
            a.FloorNumber = dto.FloorNumber;
            a.ApartmentNumber = dto.ApartmentNumber;
            a.AdditionalDirections = dto.AdditionalDirections;
            a.Label = dto.Label;
            a.Latitude = dto.Latitude;
            a.Longitude = dto.Longitude;

            await _unitOfWork.Address.UpdateAsync(a, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAddressAsync(int id, string userId, CancellationToken cancellationToken = default)
        {
            var a = await _unitOfWork.Address.GetByIdAsync(id, cancellationToken);
            if (a == null || a.UserId != userId) return false;

            await _unitOfWork.Address.DeleteAsync(a, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync(CancellationToken cancellationToken = default)
        {
            var addresses = await _unitOfWork.Address.GetAllAsync(cancellationToken);
            return addresses.Select(a => new AddressDto
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
            }).ToList();
        }

        public async Task<bool> DeleteAddressAsync(int id, CancellationToken cancellationToken = default)
        {
            var a = await _unitOfWork.Address.GetByIdAsync(id, cancellationToken);
            if (a == null) return false;

            await _unitOfWork.Address.DeleteAsync(a, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}