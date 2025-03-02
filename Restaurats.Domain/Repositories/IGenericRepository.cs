using System.Linq.Expressions;

namespace Restaurats.Domain.Repositories;
public interface IGenericRepository<T> where T:class
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>?predicate=null,string ?includeObjs = null, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(Expression<Func<T, bool>>? predicate=null, string? includeObjs=null,CancellationToken cancellationToken=default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity);
}
