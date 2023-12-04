using MediatR;

namespace Shopmaster.Application.Commands.Accounts.Me;

public record AccountsMeRequest(
) : IRequest<AccountsMeResponse>;