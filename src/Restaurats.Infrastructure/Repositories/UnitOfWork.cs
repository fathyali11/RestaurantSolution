using Restaurats.Domain;
using Restaurats.Domain.Repositories;
using Restaurats.Infrastructure.Presistence;

namespace Restaurats.Infrastructure.Repositories;
internal class UnitOfWork(RestaurantDbContext context) :IUnitOfWork
{
    private readonly RestaurantDbContext _context=context;
    private readonly IRestaurantRepository? _restaurant;
    private readonly IDishRepository? _dish;

    public IRestaurantRepository Restaurant =>
        _restaurant ?? new RestaurantRepository(_context);

    public IDishRepository Dish =>
        _dish ?? new DishRepository(_context);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
