using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Exceptions;

namespace Restaurats.Application.ApplicationUsers.Commands.RemoveUserRoles;
internal class RemoveUserRoleCommandHandler(
    ILogger<RemoveUserRoleCommandHandler> logger,
    UserManager<ApplicationUser>userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<RemoveUserRoleCommand>
{
    private readonly ILogger<RemoveUserRoleCommandHandler> _logger = logger;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Assignning user role {@request}",request);

        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

        var role = await _roleManager.FindByNameAsync(request.RoleName)
            ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

        await _userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}
