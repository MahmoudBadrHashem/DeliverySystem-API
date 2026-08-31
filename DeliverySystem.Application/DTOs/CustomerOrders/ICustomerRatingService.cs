using DeliverySystem.Application.DTOs.CustomerOrders;

namespace DeliverySystem.Application.Interfaces
{
    public interface ICustomerRatingService
    {
        Task<int> RateOrderAsync(string userId, CreateCustomerRatingDto dto);
    }
}