using MediatR;

namespace Shopmaster.Application.Commands.Auth.Recovery;

public record AuthRecoveryRequest(
    string Email
) : IRequest;