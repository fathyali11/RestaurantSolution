using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Restaurats.APITests;
internal class FakePolicyEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(
                new[]
                         {
                             new Claim(ClaimTypes.NameIdentifier, "1"),
                             new Claim(ClaimTypes.Role, "Admin")
                         }, 
                "FakeScheme")
            );

        var authenticationTicket = new AuthenticationTicket(claimsPrincipal, "FakeScheme");

        return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
    {
        return Task.FromResult(PolicyAuthorizationResult.Success());
    }
}
