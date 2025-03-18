using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurats.Application.ApplicationUsers.Commands;
using Restaurats.Application.ApplicationUsers.Commands.AssignUserRoles;
using Restaurats.Application.ApplicationUsers.Commands.RemoveUserRoles;

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

    [HttpPost("user_role")]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("remove-user_role")]
    public async Task<IActionResult> RemoveUserRole(RemoveUserRoleCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
