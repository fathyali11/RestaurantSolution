using Restaurats.Domain.Repositories;
using Restaurats.Infrastructure.Presistence;

namespace Restaurats.Infrastructure.Repositories;
internal class UnitOfWork(RestaurantDbContext context) :IUnitOfWork
{
    private readonly RestaurantDbContext _context=context;
    private readonly IRestaurantRepository? _repository;

    public IRestaurantRepository Restaurant =>
        _repository??new RestaurantRepository(_context);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
