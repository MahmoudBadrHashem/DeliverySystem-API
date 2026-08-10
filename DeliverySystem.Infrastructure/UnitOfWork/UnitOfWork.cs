using DeliverySystem.Application.Interfaces;
using DeliverySystem.Infrastructure.Persistence;
using DeliverySystem.Infrastructure.Repositories;

namespace DeliverySystem.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IRefreshTokenRepository RefreshToken { get; private set; }
    public IAddressRepository Address { get; private set; }
    public ICouponRepository Coupon { get; private set; }
    public INotificationRepository Notification { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        RefreshToken = new RefreshTokenRepository(context);
        Address = new AddressRepository(context);
        Coupon = new CouponRepository(context);
        Notification = new NotificationRepository(context);
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}