using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Exceptions;

namespace Restaurats.Application.ApplicationUsers.Commands;
internal class UpdateUserDetailsCommandHandler(IUserContext userContext,
    ILogger<UpdateUserDetailsCommandHandler> logger,
    UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateUserDetailsCommand>
{
    private readonly IUserContext _userContext = userContext;
    private readonly ILogger<UpdateUserDetailsCommandHandler> _logger = logger;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var userId=_userContext.GetCurrentUser()?.Id;
        if (userId == null)
            throw new NotFoundException(nameof(ApplicationUser),string.Empty);
        _logger.LogInformation("update user with {id} in {request}", userId, request);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new NotFoundException(nameof(ApplicationUser), userId);

        user.DateOfBirth = request.DateOfBirth;
        user.Nationality = request.Nationality;

        await _userManager.UpdateAsync(user);
    }
}
