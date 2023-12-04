using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopmaster.Application.Commands.Auth;
using Shopmaster.Application.Commands.Auth.Confirm;
using Shopmaster.Application.Commands.Auth.Login;
using Shopmaster.Application.Commands.Auth.Logout;
using Shopmaster.Application.Commands.Auth.Recovery;
using Shopmaster.Application.Commands.Auth.RecoveryPassword;
using Shopmaster.Application.Commands.Auth.Refresh;
using Shopmaster.Application.Commands.Auth.Register;

namespace Shopmaster.Api.Controllers.v1;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] AuthRegisterRequest request)
    {
        return Ok(_mediator.Send(request));
    }

    [HttpPost("login")]
    public ActionResult<AuthLoginResponse> Login([FromBody] AuthLoginRequest request)
    {
        return Ok(_mediator.Send(request));
    }

    [HttpPost("recovery")]
    public IActionResult Recovery([FromBody] AuthRecoveryRequest request)
    {
        return Ok(_mediator.Send(request));
    }

    [HttpPost("recovery/{link:guid}")]
    public IActionResult Recovery([FromRoute] Guid link, [FromBody] string password)
    {
        return Ok(_mediator.Send(new AuthRecoveryPasswordRequest(link, password)));
    }

    [HttpPost("confirm/{link}")]
    public IActionResult Confirm([FromRoute] string link)
    {
        return Ok(_mediator.Send(new AuthConfirmRequest(link)));
    }

    [Authorize]
    [HttpPost("refresh")]
    public ActionResult<AuthRefreshResponse> Refresh()
    {
        return Ok(_mediator.Send(new AuthRefreshRequest()));
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return Ok(_mediator.Send(new AuthLogoutRequest()));
    }
}