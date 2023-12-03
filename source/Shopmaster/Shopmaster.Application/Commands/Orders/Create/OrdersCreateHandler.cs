using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Orders.Create;

public class OrdersCreateHandler : IRequestHandler<OrdersCreateRequest, OrdersCreateResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAdvertRepository _advertRepository;
    private readonly IOrderRepository _orderRepository;

    public OrdersCreateHandler(IHttpContextAccessor httpContextAccessor, IAdvertRepository advertRepository, IOrderRepository orderRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _advertRepository = advertRepository;
        _orderRepository = orderRepository;
    }

    public Task<OrdersCreateResponse> Handle(OrdersCreateRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
        {
            throw new ApplicationException("Unauthorized");
        }

        if (_advertRepository.GetById(request.AdvertId) is not Advert advert)
        {
            throw new ApplicationException("Advert with given id not found");
        }

        Order order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = userId,
            AdvertId = advert.Id,
            DateCreated = DateTime.UtcNow,
        };

        _orderRepository.Add(order);

        return Task.FromResult(
            new OrdersCreateResponse(
                Id: order.Id.ToString()
            )
        );
    }
}
