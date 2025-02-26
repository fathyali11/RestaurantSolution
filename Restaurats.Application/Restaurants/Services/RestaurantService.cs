using Mapster;
using MapsterMapper;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Restaurants.Services;
public class RestaurantService(IUnitOfWork unitOfWork,IMapper mapper) : IRestaurantService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork= unitOfWork;
    public async Task<RestaurantResponse> CreateRestaurant(CreateRestaurantRequest request, CancellationToken cancellationToken = default)
    {
        var restaurant = request.Adapt<Restaurant>();
        restaurant= await _unitOfWork.Restaurant.AddAsync(restaurant,cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return restaurant.Adapt<RestaurantResponse>();
    }

    public Task DeleteRestaurant(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<RestaurantResponse>> GetAllRestaurants(CancellationToken cancellationToken = default)
    {
        var restaurants = await _unitOfWork
        .Restaurant
        .GetAllAsync(cancellationToken);

        var response1=_mapper.Map<IEnumerable<RestaurantResponse>>(restaurants);
        var response2 = restaurants.Adapt<IEnumerable<RestaurantResponse>>();
        return response1;
    }

    public Task<RestaurantResponse> GetRestaurant(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<RestaurantResponse> UpdateRestaurant(int id, CreateRestaurantRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
