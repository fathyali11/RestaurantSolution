using Mapster;
using MediatR;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantCommandHandler(IUnitOfWork unitOfWork) :
    IRequestHandler<CreateRestaurantCommand, RestaurantResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<RestaurantResponse> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = request.Adapt<Restaurant>();
        restaurant = await _unitOfWork.Restaurant.AddAsync(restaurant, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return restaurant.Adapt<RestaurantResponse>();
    }
}
