using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Adverts.Edit;

public class AdvertsEditHandler : IRequestHandler<AdvertsEditRequest, AdvertsEditResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAdvertRepository _advertRepository;

    public AdvertsEditHandler(IAdvertRepository advertRepository, IHttpContextAccessor httpContextAccessor)
    {
        _advertRepository = advertRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<AdvertsEditResponse> Handle(AdvertsEditRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
        {
            throw new ApplicationException("Unauthorized");
        }

        if (_advertRepository.GetById(request.Id) is not Advert advert)
        {
            throw new ApplicationException("Advert with given id not found");
        }

        if (advert.SellerId != userId)
        {
            throw new ApplicationException("No access");
        }

        advert.Title = request.Title;
        advert.Description = request.Description;
        advert.Location = request.Location;
        advert.Price = request.Price;
        advert.CategoryId = request.CategoryId;
        _advertRepository.Update(advert);

        return Task.FromResult(
            new AdvertsEditResponse(
                Id: advert.Id.ToString()
            )
        );
    }
}
