using MediatR;

namespace Shopmaster.Application.Commands.Accounts.EditPassword;

public record AccountsEditPasswordRequest(
    string OldPassword,
    string NewPassword
) : IRequest;