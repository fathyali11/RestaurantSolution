using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurats.API.UpdateRestaurant;
using Restaurats.Application.Restaurants.Commands.CreateRestaurant;
using Restaurats.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurats.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurats.Application.Restaurants.Queries.GetRestaurant;
using Restaurats.Domain.Constants;
using Restaurats.Infrastructure.Authorization.Constants;

namespace Restaurats.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = UserRoles.AdminRole)]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantCommand request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        if(response is null)
            return BadRequest();
        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }
    [HttpPut("{id}")]
    [Authorize(Roles = UserRoles.AdminRole)]
    public async Task<IActionResult> Update([FromRoute]int id,[FromBody] UpdateRestaurantCommand request, CancellationToken cancellationToken = default)
    {
        request.Id = id;
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]GetAllRestaurantsQuery query,CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = PolicyNames.HasNationality)]
    public async Task<IActionResult> Get([FromRoute]int id,CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetRestaurantQuery(id), cancellationToken);
        return Ok(response);
    }
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteRestaurantCommand(id), cancellationToken);
        return NoContent();
    }
}
