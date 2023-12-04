using MediatR;

namespace Shopmaster.Application.Commands.Auth.RecoveryPassword;

public record AuthRecoveryPasswordRequest(
    Guid Id,
    string Password
) : IRequest;