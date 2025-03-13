namespace Restaurats.Application.ApplicationUsers;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}