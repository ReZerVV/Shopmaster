using MediatR;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Orders.GetById;

public class OrdersGetByIdHandler : IRequestHandler<OrdersGetByIdRequest, OrdersGetByIdResponse>
{
    private readonly IOrderRepository _orderRepository;

    public OrdersGetByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<OrdersGetByIdResponse> Handle(OrdersGetByIdRequest request, CancellationToken cancellationToken)
    {
        if (_orderRepository.GetById(request.Id) is not Order order)
        {
            throw new ApplicationException("Order with given id not found");
        }

        return Task.FromResult(
            new OrdersGetByIdResponse(
                Id: order.Id.ToString(),
                CustomerId: order.CustomerId.ToString(),
                AdvertId: order.AdvertId.ToString(),
                DateCreated: order.DateCreated,
                Status: order.Status.ToString(),
                StatusCode: (int)order.Status
            )
        );
    }
}
