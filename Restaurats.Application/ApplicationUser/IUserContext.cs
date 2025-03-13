namespace Restaurats.Application.ApplicationUser;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}