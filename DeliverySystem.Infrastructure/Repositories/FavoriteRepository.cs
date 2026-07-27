using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Favorite>> GetCustomerFavoritesAsync(int customerId) =>
            await _context.Favorites
                .Where(f => f.CustomerId == customerId)
                .Include(f => f.Product)
                .ToListAsync();

        public async Task<bool> ExistsAsync(int customerId, int productId) =>
            await _context.Favorites
                .AnyAsync(f => f.CustomerId == customerId && f.ProductId == productId);

        public async Task AddAsync(Favorite favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveAsync(int customerId, int productId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.CustomerId == customerId && f.ProductId == productId);

            if (favorite == null) return false;

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}