using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Restaurants.Commands.DeleteRestaurant;
internal class DeleteRestaurantCommandHandler(IUnitOfWork unitOfWork,ILogger<DeleteRestaurantCommandHandler>logger) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger = logger;
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Restaurant From Database");
        var restaurantFromDb = await _unitOfWork.Restaurant.GetByIdAsync(request.Id);
        if (restaurantFromDb is null)
        {
            _logger.LogInformation("Restaurant Not Found");
            return false;
        }
        _logger.LogInformation("try to delete restaurant from db");
        await _unitOfWork.Restaurant.DeleteAsync(restaurantFromDb);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("restaurant was deleted from db");
        return true;
    }
}
