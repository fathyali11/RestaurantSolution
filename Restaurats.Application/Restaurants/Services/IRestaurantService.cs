using Restaurats.Application.Restaurants.Commands.CreateRestaurant;
using Restaurats.Application.Restaurants.Dtos;

namespace Restaurats.Application.Restaurants.Services;
public interface IRestaurantService
{
    Task<IEnumerable<RestaurantResponse>> GetAllRestaurants(CancellationToken cancellationToken = default);
    Task<RestaurantResponse?> GetRestaurant(int id,CancellationToken cancellationToken = default);
    Task<RestaurantResponse> UpdateRestaurant(int id,CreateRestaurantCommand request,CancellationToken cancellationToken = default);
    Task DeleteRestaurant(int id,CancellationToken cancellationToken = default);



}
