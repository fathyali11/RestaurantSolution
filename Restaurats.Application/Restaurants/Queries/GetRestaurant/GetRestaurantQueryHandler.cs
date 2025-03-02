using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Exceptions;
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
            .Restaurant.GetByIdAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (restaurant is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        _logger.LogInformation("Restaurant Found");
        return _mapper.Map<RestaurantResponse>(restaurant!);
    }
}
