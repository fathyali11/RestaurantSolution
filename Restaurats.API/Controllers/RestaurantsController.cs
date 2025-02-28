using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurats.Application.Restaurants.Commands.CreateRestaurant;
using Restaurats.Application.Restaurants.Services;

namespace Restaurats.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RestaurantsController(IRestaurantService restaurantService,IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IRestaurantService _restaurantService = restaurantService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        if(response is null)
            return BadRequest();
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await _restaurantService.GetAllRestaurants(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id,CancellationToken cancellationToken = default)
    {
        var response = await _restaurantService.GetRestaurant(id,cancellationToken);
        return Ok(response);
    }
}
