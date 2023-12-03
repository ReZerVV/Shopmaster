using MediatR;
using Shopmaster.Application.Common.Persistence;

namespace Shopmaster.Application.Commands.Orders.GetByAdvertId;

public class OrdersGetByAdvertIdHandler : IRequestHandler<OrdersGetByAdvertIdRequest, IEnumerable<OrdersGetByAdvertIdResponse>>
{
    private readonly IOrderRepository _orderRepository;

    public OrdersGetByAdvertIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<IEnumerable<OrdersGetByAdvertIdResponse>> Handle(OrdersGetByAdvertIdRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<OrdersGetByAdvertIdResponse> orders = _orderRepository.GetByAdvertId(request.AdvertId)
            .Select(order => new OrdersGetByAdvertIdResponse(
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
