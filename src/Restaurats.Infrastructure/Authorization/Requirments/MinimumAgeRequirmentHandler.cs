using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurats.Application.ApplicationUsers;

namespace Restaurats.Infrastructure.Authorization.Requirments;
internal class MinimumAgeRequirmentHandler(IUserContext userContext,
    ILogger<MinimumAgeRequirmentHandler> logger) : AuthorizationHandler<MinimumAgeRequirment>
{
    private readonly IUserContext _userContext = userContext;
    private readonly ILogger<MinimumAgeRequirmentHandler> _logger = logger;
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirment requirement)
    {
        var currentUser = _userContext.GetCurrentUser();

        _logger.LogInformation("Checking user age");
        if (currentUser!.Date is null)
        {
            _logger.LogInformation("User date of birth is not set");
            context.Fail();
            return Task.CompletedTask;
        }

        var age = DateTime.Today.Year - currentUser.Date.Value.Year;

        if(age >= requirement.MinimumAge)
        {
            _logger.LogInformation("User is old enough");
            context.Succeed(requirement);
        }
        else
        {
            _logger.LogInformation("User is not old enough");
            context.Fail();
        }
        return Task.CompletedTask;
    }
}
