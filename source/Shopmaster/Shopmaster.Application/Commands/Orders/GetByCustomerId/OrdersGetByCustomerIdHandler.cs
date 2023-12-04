using MediatR;
using Shopmaster.Application.Common.Persistence;

namespace Shopmaster.Application.Commands.Orders.GetByCustomerId;

public class OrdersGetByCustomerIdHandler : IRequestHandler<OrdersGetByCustomerIdRequest, IEnumerable<OrdersGetByCustomerIdResponse>>
{
    private readonly IOrderRepository _orderRepository;

    public OrdersGetByCustomerIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<IEnumerable<OrdersGetByCustomerIdResponse>> Handle(OrdersGetByCustomerIdRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<OrdersGetByCustomerIdResponse> orders = _orderRepository.GetByCustomerId(request.CustomerId)
            .Select(order => new OrdersGetByCustomerIdResponse(
                Id: order.Id.ToString(),
                CustomerId: order.CustomerId.ToString(),
                AdvertId: order.AdvertId.ToString(),
                DateCreated: order.DateCreated,
                Status: order.Status.ToString(),
                StatusCode: (int)order.Status
            ));

        return Task.FromResult(orders);
    }
}
