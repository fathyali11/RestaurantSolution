using MediatR;
using Restaurats.Application.Dishes.Dtos;

namespace Restaurats.Application.Dishes.Commands.CreateDish;
public record CreateDishCommand(
    string Name,
    string Description,
    decimal Price,
    int KiloCalories,
    int RestaurantId
) : IRequest<DishResponse>
{
    public int RestaurantId { get; set; } = RestaurantId;
}