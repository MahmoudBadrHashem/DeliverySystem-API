using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int customerId);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(int status);
        Task<Order?> GetOrderWithDetailsAsync(int orderId);
    }
}