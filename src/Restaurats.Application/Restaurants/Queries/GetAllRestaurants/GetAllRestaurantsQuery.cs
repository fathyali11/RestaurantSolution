using MediatR;
using Restaurats.Application.Common;
using Restaurats.Application.Restaurants.Dtos;

namespace Restaurats.Application.Restaurants.Queries.GetAllRestaurants;
public record GetAllRestaurantsQuery(string ?SearchTerm,string ?SortBy, string ?OrderDirection,int ?PageNumber,int ?PageSize)
    :IRequest<PaginatedResult<RestaurantWithDishesResponse>>;

