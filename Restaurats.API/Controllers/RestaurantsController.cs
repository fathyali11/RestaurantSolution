using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Application.Restaurants.Services;

namespace Restaurats.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
{
    private readonly IRestaurantService _restaurantService = restaurantService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _restaurantService.CreateRestaurant(request, cancellationToken);
        return Ok(response);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await _restaurantService.GetAllRestaurants(cancellationToken);
        return Ok(response);
    }   
}
