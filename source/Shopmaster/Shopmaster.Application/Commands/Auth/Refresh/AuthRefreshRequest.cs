using MediatR;

namespace Shopmaster.Application.Commands.Auth.Refresh;

public record AuthRefreshRequest(
) : IRequest<AuthRefreshResponse>;