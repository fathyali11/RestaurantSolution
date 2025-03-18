using MediatR;
namespace Restaurats.API.UpdateRestaurant;

public record UpdateRestaurantCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; init; }= string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool HasDelivery { get; init; }
}