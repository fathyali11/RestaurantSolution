using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurats.Application.ApplicationUsers.Commands;

namespace Restaurats.API.Controllers;
[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator): ControllerBase
{
    private readonly IMediator _mediator=  mediator;

    [HttpPut("user")]
    public async Task<IActionResult> Update(UpdateUserDetailsCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
