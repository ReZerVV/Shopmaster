using MediatR;

namespace Shopmaster.Application.Commands.Auth;

public record AuthLogoutRequest(
) : IRequest;