namespace Restaurats.Application.ApplicationUsers;
public record CurrentUser(string Id, string Email, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
