using Microsoft.EntityFrameworkCore;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Payment?> GetByOrderIdAsync(int orderId, CancellationToken cancellationToken = default) =>
            await _context.Payments
                .FirstOrDefaultAsync(p => p.OrderId == orderId, cancellationToken);
    }
}