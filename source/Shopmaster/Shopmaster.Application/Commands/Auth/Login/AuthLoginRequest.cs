using MediatR;

namespace Shopmaster.Application.Commands.Auth.Login;

public record AuthLoginRequest(
    string Email,
    string Password
) : IRequest<AuthLoginResponse>;