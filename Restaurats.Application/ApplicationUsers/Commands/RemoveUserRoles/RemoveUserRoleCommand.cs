using MediatR;

namespace Restaurats.Application.ApplicationUsers.Commands.RemoveUserRoles;
public record RemoveUserRoleCommand(string Email,string RoleName): IRequest;

