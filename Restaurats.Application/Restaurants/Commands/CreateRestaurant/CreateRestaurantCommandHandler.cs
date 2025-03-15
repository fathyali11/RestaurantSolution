using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Application.ApplicationUsers;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Constants;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Interfaces;
using Restaurats.Domain.Repositories;
using Restaurats.Domain.Exceptions;

namespace Restaurats.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantCommandHandler(IUnitOfWork _unitOfWork,
    ILogger<CreateRestaurantCommandHandler> _logger,IUserContext _userContext,
    IRestaurantAuthorizationService _restaurantAuthorizationService) 
    :IRequestHandler<CreateRestaurantCommand, RestaurantResponse>
{
   
    public async Task<RestaurantResponse> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser();


        var restaurant = request.Adapt<Restaurant>();
        restaurant.OwnerId = currentUser!.Id;
        _logger.LogInformation("Get Restaurants From Database");

        if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Create))
            throw new ForbiddenException();
        restaurant = await _unitOfWork.Restaurant.AddAsync(restaurant, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Restaurant Created Successfully");
        return restaurant.Adapt<RestaurantResponse>();
    }
}
