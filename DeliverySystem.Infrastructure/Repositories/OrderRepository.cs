using Microsoft.EntityFrameworkCore;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId) =>
    await _context.Orders
        .Where(o => o.CustomerId == customerId)
        .ToListAsync();

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(int status) =>
            await _context.Orders
                .Where(o => (int)o.Status == status)
                .ToListAsync();

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId) =>
            await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}