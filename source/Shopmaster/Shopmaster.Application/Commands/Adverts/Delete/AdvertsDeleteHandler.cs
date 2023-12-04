using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Adverts.Delete;

public class AdvertsDeleteHandler : IRequestHandler<AdvertsDeleteRequest, AdvertsDeleteResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAdvertRepository _advertRepository;

    public AdvertsDeleteHandler(IAdvertRepository advertRepository, IHttpContextAccessor httpContextAccessor)
    {
        _advertRepository = advertRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<AdvertsDeleteResponse> Handle(AdvertsDeleteRequest request, CancellationToken cancellationToken)
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

        _advertRepository.Remove(advert);

        return Task.FromResult(
            new AdvertsDeleteResponse()
        );
    }
}
