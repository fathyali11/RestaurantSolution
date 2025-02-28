using MapsterMapper;
using MediatR;
using Restaurats.Domain.Repositories;

namespace Restaurats.API.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(IUnitOfWork unitOfWork,IMapper mapper) :
    IRequestHandler<UpdateRestaurantCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantFromDb = await _unitOfWork.Restaurant.GetByIdAsync(request.Id);
        if (restaurantFromDb is null)
            return false;

        var restaurant = _mapper.Map(request, restaurantFromDb);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
