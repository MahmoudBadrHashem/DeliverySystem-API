using DeliverySystem.Application.DTOs.CustomerOrders;

namespace DeliverySystem.Application.Interfaces
{
    public interface ICustomerOrderService
    {
        Task<int> CreateOrderAsync(string userId, CreateCustomerOrderDto dto);
        Task<IEnumerable<CustomerOrderDto>> GetMyOrdersAsync(string userId);
        Task<CustomerOrderDto?> GetMyOrderByIdAsync(string userId, int orderId);
        Task<bool> CancelOrderAsync(string userId, int orderId);
    }
}