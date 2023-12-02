using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Adverts.Activate;

public class AdvertsActivateHandler : IRequestHandler<AdvertsActivateRequest, AdvertsActivateResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAdvertRepository _advertRepository;

    public AdvertsActivateHandler(IAdvertRepository advertRepository, IHttpContextAccessor httpContextAccessor)
    {
        _advertRepository = advertRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<AdvertsActivateResponse> Handle(AdvertsActivateRequest request, CancellationToken cancellationToken)
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

        advert.IsActive = true;
        _advertRepository.Update(advert);

        return Task.FromResult(
            new AdvertsActivateResponse()
        );
    }
}
