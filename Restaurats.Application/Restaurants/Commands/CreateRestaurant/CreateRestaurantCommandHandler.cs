using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantCommandHandler(IUnitOfWork unitOfWork,ILogger<CreateRestaurantCommandHandler> logger) :
    IRequestHandler<CreateRestaurantCommand, RestaurantResponse>
{
    private readonly ILogger<CreateRestaurantCommandHandler> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<RestaurantResponse> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = request.Adapt<Restaurant>();
        _logger.LogInformation("Get Restaurants From Database");
        restaurant = await _unitOfWork.Restaurant.AddAsync(restaurant, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Restaurant Created Successfully");
        return restaurant.Adapt<RestaurantResponse>();
    }
}
