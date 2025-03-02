using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Exceptions;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Restaurants.Commands.DeleteRestaurant;
internal class DeleteRestaurantCommandHandler(IUnitOfWork unitOfWork,ILogger<DeleteRestaurantCommandHandler>logger) 
    : IRequestHandler<DeleteRestaurantCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger = logger;
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Restaurant From Database");
        var restaurantFromDb = await _unitOfWork.Restaurant.GetByIdAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (restaurantFromDb is null)
            throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
        _logger.LogInformation("try to delete restaurant from db");
        await _unitOfWork.Restaurant.DeleteAsync(restaurantFromDb);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("restaurant was deleted from db");
    }
}
