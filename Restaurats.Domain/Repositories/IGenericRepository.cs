namespace Restaurats.Domain.Repositories;
public interface IGenericRepository<T> where T:class
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity);
}
