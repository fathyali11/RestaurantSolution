using MediatR;

namespace Restaurats.Application.ApplicationUsers.Commands.AssignUserRoles;
public record AssignUserRoleCommand(string Email,string RoleName): IRequest;

