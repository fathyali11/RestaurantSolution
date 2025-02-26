namespace Restaurats.Application.Dishes.Dtos;
public record CreateDishRequest(
    string Name,
    string Description,
    decimal Price,
    int KiloCalories
);
