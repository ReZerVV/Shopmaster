namespace Shopmaster.Application.Commands.Orders.GetById;

public record OrdersGetByIdResponse(
    string Id,
    string CustomerId,
    string AdvertId,
    DateTime DateCreated,
    string Status,
    int StatusCode
);