using DeliverySystem.Application.DTOs.Merchants;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.DTOs.Merchants
{
    public static class MerchantMapper
    {
        public static Merchant ToEntity(this CreateMerchantDto dto)
        {
            return new Merchant
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone
            };
        }

        public static MerchantDto ToDto(this Merchant merchant)
        {
            return new MerchantDto
            {
                Id = merchant.Id,
                Name = merchant.Name,
                Email = merchant.Email,
                Phone = merchant.Phone
            };
        }
    }
}