using Restaurats.Domain.Entities;

namespace Restaurats.Domain.Repositories;
public interface IRestaurantRepository: IGenericRepository<Restaurant>
{
    Task<IEnumerable<Restaurant>> GetAllPagedAsync(string? searchTerm, string? sortBy, string? orderBy, CancellationToken cancellationToken = default);
}
