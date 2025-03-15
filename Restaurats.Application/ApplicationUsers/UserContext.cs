using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurats.Application.ApplicationUsers;
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
        var nationality = user.FindFirstValue("nationality");
        var dateAsString = user.FindFirstValue("dateOfBirth");
        var date =dateAsString is null ?(DateOnly?) null : DateOnly.ParseExact(dateAsString, "yyyy-MM-dd");

        return new CurrentUser(id!, email!, roles,nationality,date);
        
        
    }
}

