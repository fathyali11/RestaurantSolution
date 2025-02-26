namespace Restaurats.Application.Dishes.Dtos;
public record DishResponse(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int KiloCalories
    );
