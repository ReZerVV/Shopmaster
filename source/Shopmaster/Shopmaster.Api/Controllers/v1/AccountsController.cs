using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopmaster.Application.Commands.Accounts.Delete;
using Shopmaster.Application.Commands.Accounts.Edit;
using Shopmaster.Application.Commands.Accounts.GetById;
using Shopmaster.Application.Commands.Accounts.Me;

namespace Shopmaster.Api.Controllers;

[ApiController]
[Route("api/v1/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet("me")]
    public ActionResult<AccountsMeResponse> Me()
    {
        return Ok(_mediator.Send(new AccountsMeRequest()));
    }

    [Authorize]
    [HttpPut("{userId:guid}")]
    public ActionResult<AccountsEditResponse> Edit([FromBody] AccountsEditRequest request)
    {
        return Ok(_mediator.Send(request));
    }

    [Authorize]
    [HttpDelete]
    public IActionResult Delete()
    {
        return Ok(_mediator.Send(new AccountsDeleteRequest()));
    }

    [HttpGet("{userId:guid}")]
    public ActionResult GetById([FromRoute] Guid userId)
    {
        return Ok(_mediator.Send(new AccountsGetByIdRequest(userId)));
    }
}