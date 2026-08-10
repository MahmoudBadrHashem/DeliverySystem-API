

using System.Linq.Expressions;
using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Interfaces;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<IEnumerable<RefreshToken>> GetAll(Expression<Func<RefreshToken, bool>>? criteria = null, CancellationToken cancellationToken = default);
    Task<RefreshToken?> GetFirstOneAsync(Expression<Func<RefreshToken, bool>>? criteria, CancellationToken cancellationToken = default);
}