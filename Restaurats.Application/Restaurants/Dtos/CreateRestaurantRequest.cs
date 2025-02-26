using Restaurats.Application.Dishes.Dtos;

namespace Restaurats.Application.Restaurants.Dtos;
public record CreateRestaurantRequest(
    string Name,
    string Description,
    string Category,
    string City,
    string Street,
    string PostalCode,
    string LogoSasUrl,
    List<CreateDishRequest> Dishes
);
