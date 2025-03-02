using MediatR;

namespace Restaurats.Application.Dishes.Commands.DeleteDishes;
public record DeleteDishesForRestaurantCommand(int restaurantId) :IRequest
{
    public int RestaurantId { get; set; }=restaurantId;
}
