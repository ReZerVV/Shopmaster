namespace Shopmaster.Application.Commands.Accounts.GetById;

public record AccountGetByIdResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email
);