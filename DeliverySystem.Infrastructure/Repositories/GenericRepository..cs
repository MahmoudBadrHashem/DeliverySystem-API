using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Infrastructure.Persistence;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
            await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _context.Set<T>().ToListAsync(cancellationToken);

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
        }

        public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
    }
}