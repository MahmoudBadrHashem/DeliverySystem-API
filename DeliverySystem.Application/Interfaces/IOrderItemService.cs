using DeliverySystem.Application.DTOs.OrderItems;

namespace DeliverySystem.Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync(CancellationToken cancellationToken = default);
        Task<OrderItemDto?> GetOrderItemByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CreateOrderItemAsync(CreateOrderItemDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteOrderItemAsync(int id, CancellationToken cancellationToken = default);
    }
}