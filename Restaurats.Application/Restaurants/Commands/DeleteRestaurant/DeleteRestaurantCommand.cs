using MediatR;

namespace Restaurats.Application.Restaurants.Commands.DeleteRestaurant;
public record DeleteRestaurantCommand(int Id) : IRequest<bool>;
