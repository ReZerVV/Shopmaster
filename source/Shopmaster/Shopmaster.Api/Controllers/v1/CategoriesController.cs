using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopmaster.Application.Commands.Categories.Create;
using Shopmaster.Application.Commands.Categories.Delete;
using Shopmaster.Application.Commands.Categories.Edit;
using Shopmaster.Application.Commands.Categories.GetAll;
using Shopmaster.Application.Commands.Categories.GetById;

namespace Shopmaster.Api.Controllers.v1;

[ApiController]
[Route("api/v1/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public ActionResult<CategoriesCreateResponse> Create([FromBody] CategoriesCreateRequest request)
    {
        return Ok(_mediator.Send(request));
    }

    [Authorize]
    [HttpPut]
    public IActionResult Edit([FromBody] CategoriesEditRequest request)
    {
        return Ok(_mediator.Send(request));
    }

    [HttpGet]
    public ActionResult<CategoriesGetAllResponse> GetAll([FromBody] CategoriesGetAllRequest request)
    {
        return Ok(_mediator.Send(request));
    }

    [HttpGet("{categoryId:int}")]
    public ActionResult<CategoriesGetByIdResponse> GetById([FromRoute] int categoryId)
    {
        return Ok(_mediator.Send(new CategoriesGetByIdRequest(categoryId)));
    }

    [Authorize]
    [HttpDelete("{categoryId:int}")]
    public IActionResult Delete([FromRoute] int categoryId)
    {
        return Ok(_mediator.Send(new CategoriesDeleteRequest(categoryId)));
    }
}