using MediatR;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Restaurants.Commands.DeleteRestaurant;
internal class DeleteRestaurantCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantFromDb = await _unitOfWork.Restaurant.GetByIdAsync(request.Id);
        if (restaurantFromDb is null)
            return false;
        await _unitOfWork.Restaurant.DeleteAsync(restaurantFromDb);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
