namespace Restaurats.Application.ApplicationUsers;
public record CurrentUser(string Id, string Email, IEnumerable<string> Roles,string?Nationality,DateOnly?Date)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
