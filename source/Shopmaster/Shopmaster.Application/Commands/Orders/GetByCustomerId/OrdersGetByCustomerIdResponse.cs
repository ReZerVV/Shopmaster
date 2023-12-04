namespace Shopmaster.Application.Commands.Orders.GetByCustomerId;

public record OrdersGetByCustomerIdResponse(
    string Id,
    string CustomerId,
    string AdvertId,
    DateTime DateCreated,
    string Status,
    int StatusCode
);