namespace DeliverySystem.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRefreshTokenRepository RefreshToken { get; }
    IAddressRepository Address { get; }
    ICouponRepository Coupon { get; }
    INotificationRepository Notification { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}