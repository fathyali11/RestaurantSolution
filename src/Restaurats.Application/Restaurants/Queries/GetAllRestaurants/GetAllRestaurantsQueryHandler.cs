using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Application.Common;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Restaurants.Queries.GetAllRestaurants;
public class GetAllRestaurantsQueryHandler(IUnitOfWork unitOfWork,
    ILogger<GetAllRestaurantsQueryHandler> logger) :
    IRequestHandler<GetAllRestaurantsQuery, PaginatedResult<RestaurantWithDishesResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<GetAllRestaurantsQueryHandler> _logger = logger;
    public async Task<PaginatedResult<RestaurantWithDishesResponse>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try To Get Restaurant From Database");
        var restaurants = await _unitOfWork
        .Restaurant
        .GetAllPagedAsync(request.SearchTerm,request.SortBy,request.OrderDirection,cancellationToken:cancellationToken);
        var response = restaurants.Adapt<IEnumerable<RestaurantWithDishesResponse>>();
        var pagedResponse=new PaginatedResult<RestaurantWithDishesResponse>(response,response.Count(), request.PageNumber, request.PageSize);
        return pagedResponse;
    }
}
