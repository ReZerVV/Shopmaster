using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopmaster.Application.Commands.Orders.Create;
using Shopmaster.Application.Commands.Orders.Edit;
using Shopmaster.Application.Commands.Orders.GetByAdvertId;
using Shopmaster.Application.Commands.Orders.GetByCustomerId;
using Shopmaster.Application.Commands.Orders.GetById;

namespace Shopmaster.Api.Controllers.v1;

[ApiController]
[Route("api/v1/orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public ActionResult<OrdersCreateResponse> Create([FromBody] OrdersCreateRequest request)
    {
        return Ok(_mediator.Send(request));
    }

    [Authorize]
    [HttpPut]
    public ActionResult<OrdersEditResponse> Edit([FromBody] OrdersEditRequest request)
    {
        return Ok(_mediator.Send(request));
    }

    [Authorize]
    [HttpGet("{orderId:guid}")]
    public ActionResult<OrdersGetByIdResponse> GetById([FromRoute] Guid orderId)
    {
        return Ok(_mediator.Send(new OrdersGetByIdRequest(orderId)));
    }

    [Authorize]
    [HttpGet]
    public ActionResult<OrdersGetByCustomerIdResponse> GetByCustomerId([FromQuery] Guid customerId)
    {
        return Ok(_mediator.Send(new OrdersGetByCustomerIdRequest(customerId)));
    }

    [Authorize]
    [HttpGet]
    public ActionResult<OrdersGetByAdvertIdResponse> GetByAdvertId([FromQuery] Guid advertId)
    {
        return Ok(_mediator.Send(new OrdersGetByAdvertIdRequest(advertId)));
    }
}