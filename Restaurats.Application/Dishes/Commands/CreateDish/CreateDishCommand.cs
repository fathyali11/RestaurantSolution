namespace Restaurats.Application.Dishes.Commands.CreateDish;
public record CreateDishCommand(
    string Name,
    string Description,
    decimal Price,
    int KiloCalories
);
