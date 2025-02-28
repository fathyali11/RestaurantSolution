using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Domain.Repositories;

namespace Restaurats.API.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(IUnitOfWork unitOfWork,IMapper mapper,ILogger<UpdateRestaurantCommandHandler>logger) :
    IRequestHandler<UpdateRestaurantCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<UpdateRestaurantCommandHandler> _logger = logger;
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Restaurant From Database");
        var restaurantFromDb = await _unitOfWork.Restaurant.GetByIdAsync(request.Id);
        if (restaurantFromDb is null)
        {
            _logger.LogInformation("Restaurant Not Found");
            return false;
        }

        var restaurant = _mapper.Map(request, restaurantFromDb);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("restaurant was updated");
        return true;
    }
}
