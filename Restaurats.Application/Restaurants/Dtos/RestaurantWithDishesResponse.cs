using Restaurats.Application.Dishes.Dtos;

namespace Restaurats.Application.Restaurants.Dtos;
public record RestaurantWithDishesResponse(
    int Id,
    string Name,
    string Description,
    string Category,
    bool HasDelivery,
    string City,
    string Street,
    string PostalCode,
    List<DishResponse> Dishes
    );
