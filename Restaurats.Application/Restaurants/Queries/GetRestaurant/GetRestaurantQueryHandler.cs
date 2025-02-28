using MapsterMapper;
using MediatR;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Restaurants.Queries.GetRestaurant;
internal class GetRestaurantQueryHandler(IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<GetRestaurantQuery, RestaurantResponse?>
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IMapper _mapper=mapper;
    public async Task<RestaurantResponse?> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await _unitOfWork
            .Restaurant.GetByIdAsync(request.Id);

        if (restaurant is null)
            return null;
        return _mapper.Map<RestaurantResponse>(restaurant!);
    }
}
