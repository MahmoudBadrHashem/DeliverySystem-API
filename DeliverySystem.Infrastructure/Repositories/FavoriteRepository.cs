using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorite>> GetCustomerFavoritesAsync(int customerId, CancellationToken cancellationToken = default) =>
            await _context.Favorites
                .Where(f => f.CustomerId == customerId)
                .Include(f => f.Product)
                .ToListAsync(cancellationToken);

        public async Task<bool> ExistsAsync(int customerId, int productId, CancellationToken cancellationToken = default) =>
            await _context.Favorites
                .AnyAsync(f => f.CustomerId == customerId && f.ProductId == productId, cancellationToken);

        public async Task AddAsync(Favorite favorite, CancellationToken cancellationToken = default)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> RemoveAsync(int customerId, int productId, CancellationToken cancellationToken = default)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.CustomerId == customerId && f.ProductId == productId, cancellationToken);

            if (favorite == null) return false;

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}