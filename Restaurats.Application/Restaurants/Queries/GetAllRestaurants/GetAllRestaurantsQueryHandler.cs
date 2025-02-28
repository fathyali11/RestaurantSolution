using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Restaurants.Queries.GetAllRestaurants;
public class GetAllRestaurantsQueryHandler(IUnitOfWork unitOfWork,
    ILogger<GetAllRestaurantsQueryHandler> logger) :
    IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<GetAllRestaurantsQueryHandler> _logger = logger;
    public async Task<IEnumerable<RestaurantResponse>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try To Get Restaurant From Database");
        var restaurants = await _unitOfWork
        .Restaurant
        .GetAllAsync(cancellationToken);
        var response = restaurants.Adapt<IEnumerable<RestaurantResponse>>();
        return response;
    }
}
