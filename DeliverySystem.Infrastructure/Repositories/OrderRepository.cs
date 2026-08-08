using Microsoft.EntityFrameworkCore;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId, CancellationToken cancellationToken = default) =>
    await _context.Orders
        .Where(o => o.UserId == customerId)
        .ToListAsync(cancellationToken);

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(int status, CancellationToken cancellationToken = default) =>
            await _context.Orders
                .Where(o => (int)o.Status == status)
                .ToListAsync(cancellationToken);

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId, CancellationToken cancellationToken = default) =>
            await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
    }
}