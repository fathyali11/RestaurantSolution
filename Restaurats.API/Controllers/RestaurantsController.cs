using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurats.API.UpdateRestaurant;
using Restaurats.Application.Restaurants.Commands.CreateRestaurant;
using Restaurats.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurats.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurats.Application.Restaurants.Queries.GetRestaurant;
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
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute]int id,[FromBody] UpdateRestaurantCommand request, CancellationToken cancellationToken = default)
    {
        request.Id = id;
        var isUpdated=await _mediator.Send(request, cancellationToken);
        return isUpdated ? NoContent() : NotFound();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetAllRestaurantsQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]int id,CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetRestaurantQuery(id), cancellationToken);
        if (response is null)
            return NotFound();
        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var isDeleted=await _mediator.Send(new DeleteRestaurantCommand(id), cancellationToken);
        return isDeleted ? NoContent() : NotFound();
    }
}
