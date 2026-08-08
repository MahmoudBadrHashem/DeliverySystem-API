using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(int status, CancellationToken cancellationToken = default);
        Task<Order?> GetOrderWithDetailsAsync(int orderId, CancellationToken cancellationToken = default);
    }
}