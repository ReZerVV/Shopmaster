using MediatR;

namespace Shopmaster.Application.Commands.Auth.Confirm;

public record AuthConfirmRequest(
    string Link
) : IRequest;