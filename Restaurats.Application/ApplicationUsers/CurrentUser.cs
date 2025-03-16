namespace Restaurats.Application.ApplicationUsers;
public class CurrentUser(string id, string email, IEnumerable<string> roles, string? nationality, DateOnly? date)
{
    public string Id { get; } = id;
    public string Email { get; } = email;
    public IEnumerable<string> Roles { get; } = roles;
    public string? Nationality { get; } = nationality;
    public DateOnly? Date { get; } = date;

    public bool IsInRole(string role) => Roles.Contains(role);
}

