using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopmaster.Api.Common.Dtos;
using Shopmaster.Application.Commands.Adverts.Activate;
using Shopmaster.Application.Commands.Adverts.Create;
using Shopmaster.Application.Commands.Adverts.Deactivate;
using Shopmaster.Application.Commands.Adverts.Delete;
using Shopmaster.Application.Commands.Adverts.Edit;
using Shopmaster.Application.Commands.Adverts.Filter;
using Shopmaster.Application.Commands.Adverts.My;
using Shopmaster.Application.Commands.GetById;

namespace Shopmaster.Api.Controllers.v1;

[ApiController]
[Route("api/v1/adverts")]
public class AdvertsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdvertsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    public ActionResult<AdvertsCreateResponse> Create([FromBody] AdvertsCreateRequest request)
    {
        return Ok(_mediator.Send(request));
    }

    [Authorize]
    [HttpPut("{advertId:guid}")]
    public ActionResult<AdvertsEditResponse> Edit([FromRoute] Guid advertId, [FromBody] AdvertsEditDto dto)
    {
        return Ok(_mediator.Send(new AdvertsEditRequest(
            Id: advertId,
            Title: dto.Title,
            Description: dto.Description,
            Location: dto.Location,
            Price: dto.Price,
            CategoryId: dto.CategoryId
        )));
    }

    [HttpGet("{advertId:guid}")]
    public ActionResult<AdvertsGetByIdResponse> GetById([FromRoute] Guid advertId)
    {
        return Ok(_mediator.Send(new AdvertsGetByIdRequest(advertId)));
    }

    [HttpGet]
    public ActionResult<AdvertsFilterResponse> Filter([FromQuery] int offset,
                                                      [FromQuery] int count,
                                                      [FromQuery] string? title = null,
                                                      [FromQuery] int? categoryId = null,
                                                      [FromQuery] Guid? sellerId = null,
                                                      [FromQuery] int? minPrice = null,
                                                      [FromQuery] int? maxPrice = null,
                                                      [FromQuery] int? minRating = null,
                                                      [FromQuery] int? maxRating = null)
    {
        return Ok(_mediator.Send(new AdvertsFilterRequest(
            Offset: offset,
            Count: count,
            Title: title,
            CategoryId: categoryId,
            SellerId: sellerId,
            maxPrice: maxPrice,
            minPrice: minPrice,
            maxRating: maxRating,
            minRating: minRating
        )));
    }

    [Authorize]
    [HttpGet("my")]
    public ActionResult<AdvertsMyResponse> My()
    {
        return Ok(_mediator.Send(new AdvertsMyRequest()));
    }

    [Authorize]
    [HttpDelete("{advertId:guid}")]
    public ActionResult<AdvertsDeleteResponse> Delete([FromRoute] Guid advertId)
    {
        return Ok(_mediator.Send(new AdvertsDeleteRequest(advertId)));
    }

    [Authorize]
    [HttpPost("{advertId:guid}/activate")]
    public ActionResult<AdvertsActivateResponse> Activate([FromRoute] Guid advertId)
    {
        return Ok(_mediator.Send(new AdvertsActivateRequest(advertId)));
    }

    [Authorize]
    [HttpPost("{advertId:guid}/deactivate")]
    public ActionResult<AdvertsDeactivateResponse> Deactivate([FromRoute] Guid advertId)
    {
        return Ok(_mediator.Send(new AdvertsDeactivateRequest(advertId)));
    }
}