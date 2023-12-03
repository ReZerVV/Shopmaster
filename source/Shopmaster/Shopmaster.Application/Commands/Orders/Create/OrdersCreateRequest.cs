using MediatR;

namespace Shopmaster.Application.Commands.Orders.Create;

public record OrdersCreateRequest(
    Guid AdvertId
) : IRequest<OrdersCreateResponse>;