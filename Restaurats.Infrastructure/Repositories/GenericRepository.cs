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
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
}
