using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Shopmaster.Api.Controllers;

[ApiController]
[Route("/api")]
public class ErrorController : ControllerBase
{
    [Route("errors")]
    public IActionResult Errors()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(
            statusCode: StatusCodes.Status400BadRequest,
            title: exception?.Message
        );
    }
}