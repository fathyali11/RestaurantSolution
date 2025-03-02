using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Application.Dishes.Dtos;
using Restaurats.Application.Dishes.Queries.GetDish;
using Restaurats.Domain.Exceptions;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Dishes.Queries.GetAllDishes;
internal class GetAllDishesQueryHandler(IUnitOfWork unitOfWork, ILogger<GetDishQueryHandler> logger) : IRequestHandler<GetAllDishesQuery, List<DishResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<GetDishQueryHandler> _logger = logger;
    public async Task<List<DishResponse>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
    {
        var restaurantFromDb = await _unitOfWork.Restaurant.GetByIdAsync(x=>x.Id== request.RestaurantId,includeObjs: "Dishes", cancellationToken);
        if (restaurantFromDb is null)
            throw new NotFoundException("Restaurant", request.RestaurantId.ToString());
        return restaurantFromDb.Dishes.Adapt<List<DishResponse>>();
    }
}
