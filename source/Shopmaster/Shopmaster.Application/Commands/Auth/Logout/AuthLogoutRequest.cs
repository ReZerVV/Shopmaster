using MediatR;

namespace Shopmaster.Application.Commands.Auth.Logout;

public record AuthLogoutRequest(
) : IRequest;