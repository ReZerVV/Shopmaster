namespace Shopmaster.Application.Commands.Orders.GetByAdvertId;

public record OrdersGetByAdvertIdResponse(
    string Id,
    string CustomerId,
    string AdvertId,
    DateTime DateCreated,
    string Status,
    int StatusCode
);