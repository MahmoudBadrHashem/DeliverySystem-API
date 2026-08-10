namespace DeliverySystem.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRefreshTokenRepository RefreshToken { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}