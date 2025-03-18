using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurats.Application.Dishes.Dtos;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Exceptions;
using Restaurats.Domain.Repositories;

namespace Restaurats.Application.Dishes.Queries.GetDish;
internal class GetDishQueryHandler(IUnitOfWork unitOfWork,ILogger<GetDishQueryHandler>logger) : IRequestHandler<GetDishQuery, DishResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<GetDishQueryHandler> _logger = logger;
    public async Task<DishResponse> Handle(GetDishQuery request, CancellationToken cancellationToken)
    {
        var restaurantFromDb = await _unitOfWork.Restaurant
            .GetByIdAsync(x=>x.Id== request.RestaurantId,includeObjs:"Dishes",cancellationToken:cancellationToken);
        if (restaurantFromDb is null)
            throw new NotFoundException("Restaurant", request.RestaurantId.ToString());

        var dishFromDb = restaurantFromDb.Dishes.FirstOrDefault(x => x.Id == request.id);
        if (dishFromDb is null)
            throw new NotFoundException(nameof(Dish), request.id.ToString());
        return dishFromDb.Adapt<DishResponse>();
    }
}
