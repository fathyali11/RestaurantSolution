using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurats.Application.ApplicationUser;
public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public CurrentUser? GetCurrentUser()
    {
        var user = _httpContextAccessor?.HttpContext?.User;
        if (user is null)
            throw new InvalidOperationException("user context is not found");

        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return null;

        var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = user.FindFirstValue(ClaimTypes.Email);
        var roles = user.FindAll(ClaimTypes.Role).Select(x => x.Value);

        return new CurrentUser(id!, email!, roles);

    }
}

