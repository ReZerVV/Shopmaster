using MediatR;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Orders.Edit;

public class OrdersEditHandler : IRequestHandler<OrdersEditRequest, OrdersEditResponse>
{
    private readonly IOrderRepository _orderRepository;

    public OrdersEditHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<OrdersEditResponse> Handle(OrdersEditRequest request, CancellationToken cancellationToken)
    {
        if (_orderRepository.GetById(request.Id) is not Order order)
        {
            throw new ApplicationException("Order with given id not found");
        }

        order.Status = request.Status;
        _orderRepository.Update(order);

        return Task.FromResult( 
            new OrdersEditResponse(
                Id: order.Id.ToString()
            )
        );
    }
}
