using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Restaurats.Domain.Repositories;
using Restaurats.Infrastructure.Presistence;

namespace Restaurats.Infrastructure.Repositories;
internal class GenericRepository<T>(RestaurantDbContext context) : IGenericRepository<T> where T : class
{
    private readonly RestaurantDbContext _context = context;
    private readonly DbSet<T> _dbSet = context.Set<T>();
    public async Task<T> AddAsync(T entity,CancellationToken cancellationToken=default)
    {
        await _dbSet.AddAsync(entity,cancellationToken);
        return entity;
    }
    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }
    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, string? includeObjs = null, CancellationToken cancellationToken = default)
    {
        var query= _dbSet.AsQueryable();
        if (predicate is not null)
            query = query.Where(predicate);
        if(!string.IsNullOrWhiteSpace(includeObjs))
        {
            var includeProperties = includeObjs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }
        return await query.ToListAsync(cancellationToken);
    }
    public async Task<T?> GetByIdAsync(Expression<Func<T, bool>>? predicate = null, string? includeObjs = null, CancellationToken cancellationToken=default)
    {
        var query = _dbSet.AsQueryable();
        if (predicate is not null)
            query = query.Where(predicate);
        if (!string.IsNullOrWhiteSpace(includeObjs))
        {
            var includeProperties = includeObjs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }
        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
