using Microsoft.EntityFrameworkCore;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Repositories;
using Restaurats.Infrastructure.Presistence;
using Restaurats.Infrastructure.Repositories;

namespace Restaurats.Domain;
internal class DishRepository(RestaurantDbContext context) : GenericRepository<Dish>(context), IDishRepository
{
    private readonly RestaurantDbContext _context = context;
    public async Task RemoveDishesForRestaurant(int restaurantId,CancellationToken cancellationToken=default)
    {
        await _context.Dishes
            .Where(x => x.RestaurantId == restaurantId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
