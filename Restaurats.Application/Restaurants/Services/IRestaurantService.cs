using Restaurats.Application.Restaurants.Dtos;

namespace Restaurats.Application.Restaurants.Services;
public interface IRestaurantService
{
    Task<IEnumerable<RestaurantResponse>> GetAllRestaurants(CancellationToken cancellationToken = default);
    Task<RestaurantResponse> GetRestaurant(int id,CancellationToken cancellationToken = default);
    Task<RestaurantResponse> UpdateRestaurant(int id,CreateRestaurantRequest request,CancellationToken cancellationToken = default);
    Task DeleteRestaurant(int id,CancellationToken cancellationToken = default);
    Task<RestaurantResponse> CreateRestaurant(CreateRestaurantRequest request, CancellationToken cancellationToken = default);



}
