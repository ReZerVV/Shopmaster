using MediatR;

namespace Shopmaster.Application.Commands.Accounts.GetById;

public record AccountsGetByIdRequest(
    Guid Id
) : IRequest<AccountGetByIdResponse>;