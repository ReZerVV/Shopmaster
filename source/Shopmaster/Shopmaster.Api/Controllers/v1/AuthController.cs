using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopmaster.Application.Commands.Auth;
using Shopmaster.Application.Commands.Auth.Login;
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
    public ActionResult<AuthRegisterResponse> Register(AuthRegisterRequest request)
    {
        return Ok(_mediator.Send(request));
    }

    [HttpPost("login")]
    public ActionResult<AuthLoginResponse> Login(AuthLoginRequest request)
    {
        return Ok(_mediator.Send(request));
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