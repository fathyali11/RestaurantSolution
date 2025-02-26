namespace Restaurats.Domain.Repositories;
public interface IUnitOfWork
{
    public IRestaurantRepository Restaurant { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
