using MediatR;
using Restaurats.Application.Restaurants.Dtos;

namespace Restaurats.Application.Restaurants.Queries.GetRestaurant;
public record GetRestaurantQuery(int Id) : IRequest<RestaurantResponse>;
