using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Domain.Constants;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Exceptions;
using Restaurats.Domain.Interfaces;
using Restaurats.Domain.Repositories;

namespace Restaurats.API.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(IUnitOfWork _unitOfWork,
    IMapper _mapper,
    ILogger<UpdateRestaurantCommandHandler>_logger,
    IRestaurantAuthorizationService _restaurantAuthorizationService) :
    IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Restaurant From Database");
        var restaurantFromDb = await _unitOfWork.Restaurant.GetByIdAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (restaurantFromDb is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        if (!_restaurantAuthorizationService.Authorize(restaurantFromDb, ResourceOperation.Update))
            throw new ForbiddenException();

        var restaurant = _mapper.Map(request, restaurantFromDb);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("restaurant was updated");
    }
}
