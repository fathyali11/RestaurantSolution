using Restaurats.Domain.Entities;

namespace Restaurats.Domain.Repositories;
public interface IDishRepository : IGenericRepository<Dish>
{
    Task RemoveDishesForRestaurant(int restaurantId, CancellationToken cancellationToken = default);
}
