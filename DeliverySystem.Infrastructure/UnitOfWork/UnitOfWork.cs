using DeliverySystem.Application.Interfaces;
using DeliverySystem.Infrastructure.Persistence;
using DeliverySystem.Infrastructure.Repositories;

namespace DeliverySystem.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IRefreshTokenRepository RefreshToken { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        RefreshToken = new RefreshTokenRepository(context);
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