using MediatR;
using Restaurats.Application.Dishes.Dtos;

namespace Restaurats.Application.Dishes.Queries.GetAllDishes;
public record GetAllDishesQuery(int restaurantId) : IRequest<List<DishResponse>>
{
    public int RestaurantId { get; set; } = restaurantId;
}
