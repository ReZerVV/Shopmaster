using MediatR;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Orders.Edit;

public record OrdersEditRequest(
    Guid Id,
    OrderStatus Status
) : IRequest<OrdersEditResponse>;