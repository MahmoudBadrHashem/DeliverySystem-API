using DeliverySystem.Application.DTOs.Orders;

namespace DeliverySystem.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<int> CreateOrderAsync(CreateOrderDto dto);
        Task<bool> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto dto);
        Task<bool> DeleteOrderAsync(int id);
    }
}