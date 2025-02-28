using Mapster;
using MediatR;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Restaurants.Queries.GetAllRestaurants;
public class GetAllRestaurantsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<IEnumerable<RestaurantResponse>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var restaurants = await _unitOfWork
        .Restaurant
        .GetAllAsync(cancellationToken);
        var response = restaurants.Adapt<IEnumerable<RestaurantResponse>>();
        return response;
    }
}
