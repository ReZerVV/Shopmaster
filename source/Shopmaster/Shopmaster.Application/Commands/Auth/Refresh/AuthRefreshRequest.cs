using MediatR;

namespace Shopmaster.Application.Commands.Auth;

public record AuthRefreshRequest(
) : IRequest<AuthRefreshResponse>;