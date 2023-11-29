using MediatR;

namespace Shopmaster.Application.Commands.Auth.Register;

public record AuthRegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<AuthRegisterResponse>;