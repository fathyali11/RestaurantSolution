using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Domain.Exceptions;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Dishes.Commands.DeleteDishes;
internal class DeleteDishesForRestaurantCommandHandler(IUnitOfWork unitOfWork,
    ILogger<DeleteDishesForRestaurantCommandHandler> logger) : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly ILogger<DeleteDishesForRestaurantCommandHandler>_logger=logger;
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantFromDb = await _unitOfWork.Restaurant.GetByIdAsync(x=>x.Id== request.RestaurantId,cancellationToken:cancellationToken);
        if (restaurantFromDb is null)
            throw new NotFoundException("Restaurant", request.RestaurantId.ToString());
        await _unitOfWork.Dish.RemoveDishesForRestaurant(request.RestaurantId,cancellationToken);
    }
}
