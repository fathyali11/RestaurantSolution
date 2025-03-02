using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Exceptions;
using Restaurats.Domain.Repositories;

namespace Restaurats.API.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(IUnitOfWork unitOfWork,IMapper mapper,ILogger<UpdateRestaurantCommandHandler>logger) :
    IRequestHandler<UpdateRestaurantCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<UpdateRestaurantCommandHandler> _logger = logger;
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Restaurant From Database");
        var restaurantFromDb = await _unitOfWork.Restaurant.GetByIdAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
        if (restaurantFromDb is null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        var restaurant = _mapper.Map(request, restaurantFromDb);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("restaurant was updated");
    }
}
