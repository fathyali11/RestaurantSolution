using Microsoft.AspNetCore.Authorization;

namespace Restaurats.Infrastructure.Authorization.Requirments;
internal class MinimumAgeRequirment(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; }= minimumAge;
}
