using MediatR;

namespace Restaurats.Application.ApplicationUsers.Commands;
public record UpdateUserDetailsCommand(DateOnly DateOfBirth, string Nationality) :IRequest;

