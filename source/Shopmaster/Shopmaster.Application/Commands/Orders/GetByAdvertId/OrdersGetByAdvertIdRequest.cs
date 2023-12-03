using MediatR;

namespace Shopmaster.Application.Commands.Orders.GetByAdvertId;

public record OrdersGetByAdvertIdRequest(
    Guid AdvertId
) : IRequest<IEnumerable<OrdersGetByAdvertIdResponse>>;