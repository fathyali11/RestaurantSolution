using MediatR;
using Restaurats.Application.Dishes.Dtos;

namespace Restaurats.Application.Dishes.Queries.GetDish;
public record GetDishQuery(int restaurantId,int id) : IRequest<DishResponse>
{
    public int RestaurantId { get; set; } = restaurantId;
}
