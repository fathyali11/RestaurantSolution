using System.Net;
using MediatR;
using Restaurats.Application.Dishes.Commands.CreateDish;
using Restaurats.Application.Restaurants.Dtos;

namespace Restaurats.Application.Restaurants.Commands.CreateRestaurant;
public record CreateRestaurantCommand(
    string Name,
    string Description,
    string Category,
    string City,
    string Street,
    string PostalCode,
    string LogoSasUrl,
    List<CreateDishCommand> Dishes
):IRequest<RestaurantResponse>;
