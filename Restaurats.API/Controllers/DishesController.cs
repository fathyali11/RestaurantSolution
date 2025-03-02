using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurats.Application.Dishes.Commands.CreateDish;
using Restaurats.Application.Dishes.Commands.DeleteDishes;
using Restaurats.Application.Dishes.Queries.GetAllDishes;
using Restaurats.Application.Dishes.Queries.GetDish;

namespace Restaurats.API.Controllers;

[Route("api/restaurants/{restaurantId}/dishes")]
[ApiController]
public class DishesController(IMediator mediator):ControllerBase
{
    private readonly IMediator _mediator=mediator;

    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] int restaurantId, [FromBody] CreateDishCommand request, CancellationToken cancellationToken = default)
    {
        request.RestaurantId = restaurantId;
        var response = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = response.Id, restaurantId = restaurantId }, response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int restaurantId, [FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetDishQuery(restaurantId,id), cancellationToken);
        return Ok(response);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromRoute]int restaurnatId,CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllDishesQuery(restaurnatId), cancellationToken);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] int restaurantId,CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId), cancellationToken);
        return NoContent();
    }
}
