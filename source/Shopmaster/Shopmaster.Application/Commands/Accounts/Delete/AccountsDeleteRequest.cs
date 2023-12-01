using MediatR;

namespace Shopmaster.Application.Commands.Accounts.Delete;

public record AccountsDeleteRequest(
) : IRequest;