using DeliverySystem.Application.DTOs.OrderItems;

namespace DeliverySystem.Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDto>> GetAllOrderItemsAsync();
        Task<OrderItemDto?> GetOrderItemByIdAsync(int id);
        Task<int> CreateOrderItemAsync(CreateOrderItemDto dto);
        Task<bool> DeleteOrderItemAsync(int id);
    }
}