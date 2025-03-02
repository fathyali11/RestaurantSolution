using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Application.Dishes.Dtos;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Exceptions;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Dishes.Commands.CreateDish;
public class CreateDishCommandHandler(IUnitOfWork unitOfWork,
    ILogger<CreateDishCommandHandler> logger) : IRequestHandler<CreateDishCommand, DishResponse>
{
    private readonly IUnitOfWork _unitOfWork= unitOfWork;
    private readonly ILogger<CreateDishCommandHandler> _logger = logger;
    public async Task<DishResponse> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var restaurantFromDb = await _unitOfWork.Restaurant.GetByIdAsync(x => x.Id == request.RestaurantId, cancellationToken: cancellationToken);
        if(restaurantFromDb is null)
            throw new NotFoundException("Restaurant", request.RestaurantId.ToString());

        var dish=request.Adapt<Dish>();
        await _unitOfWork.Dish.AddAsync(dish,cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return dish.Adapt<DishResponse>();
    }
}
