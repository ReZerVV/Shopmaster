namespace Shopmaster.Application.Commands.Accounts.Edit;

public record AccountsEditResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email
);