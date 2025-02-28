using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Restaurants.Queries.GetRestaurant;
internal class GetRestaurantQueryHandler(IUnitOfWork unitOfWork,
    IMapper mapper,ILogger<GetRestaurantQueryHandler> logger) :
    IRequestHandler<GetRestaurantQuery, RestaurantResponse?>
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IMapper _mapper=mapper;
    private readonly ILogger<GetRestaurantQueryHandler> _logger = logger;
    public async Task<RestaurantResponse?> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Try To Get Restaurant From Database");
        var restaurant = await _unitOfWork
            .Restaurant.GetByIdAsync(request.Id);

        if (restaurant is null)
        {
            _logger.LogInformation("Restaurant Not Found");
            return null;
        }
        _logger.LogInformation("Restaurant Found");
        return _mapper.Map<RestaurantResponse>(restaurant!);
    }
}
