using DeliverySystem.Application.DTOs.Orders;

namespace DeliverySystem.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<OrderDto?> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CreateOrderAsync(CreateOrderDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateOrderStatusAsync(int id, UpdateOrderStatusDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteOrderAsync(int id, CancellationToken cancellationToken = default);
    }
}