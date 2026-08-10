using System.Linq.Expressions;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeliverySystem.Infrastructure.Repositories;

public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext dbContext)
    : base(dbContext)
    {
    }
    public async Task<RefreshToken?> GetFirstOneAsync(Expression<Func<RefreshToken, bool>>? criteria, CancellationToken cancellationToken = default)
    {
        if (criteria == null)
            return await _context.RefreshTokens.FirstOrDefaultAsync(cancellationToken);

        return await _context.RefreshTokens.FirstOrDefaultAsync(criteria);
    }
    public async Task<IEnumerable<RefreshToken>> GetAll(Expression<Func<RefreshToken, bool>>? criteria = null, CancellationToken cancellationToken = default)
    {
        var entities = _context.RefreshTokens;
        if (criteria == null)
            return entities;
        return await entities.Where(criteria).ToListAsync(cancellationToken);
    }
}