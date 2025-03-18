namespace Restaurats.Domain.Repositories;
public interface IUnitOfWork
{
    public IRestaurantRepository Restaurant { get; }
    public IDishRepository Dish { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
