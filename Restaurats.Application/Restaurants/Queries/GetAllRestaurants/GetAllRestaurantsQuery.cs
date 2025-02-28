using MediatR;
using Restaurats.Application.Restaurants.Dtos;

namespace Restaurats.Application.Restaurants.Queries.GetAllRestaurants;
public class GetAllRestaurantsQuery:IRequest<IEnumerable<RestaurantResponse>>
{
}
