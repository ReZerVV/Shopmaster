using MediatR;

namespace Shopmaster.Application.Commands.Orders.GetByCustomerId;

public record OrdersGetByCustomerIdRequest(
    Guid CustomerId
) : IRequest<IEnumerable<OrdersGetByCustomerIdResponse>>;