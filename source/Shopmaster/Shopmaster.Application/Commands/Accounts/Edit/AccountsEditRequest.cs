using MediatR;

namespace Shopmaster.Application.Commands.Accounts.Edit;

public record AccountsEditRequest(
    string FirstName,
    string LastName
) : IRequest<AccountsEditResponse>;