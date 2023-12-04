namespace Shopmaster.Application.Commands.Accounts.Me;

public record AccountsMeResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email
);