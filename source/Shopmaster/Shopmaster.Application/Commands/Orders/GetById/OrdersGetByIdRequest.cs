using MediatR;

namespace Shopmaster.Application.Commands.Orders.GetById;

public record OrdersGetByIdRequest(
    Guid Id
) : IRequest<OrdersGetByIdResponse>;