using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shopmaster.Application.Common.Persistence;

namespace Shopmaster.Application.Commands.Adverts.My;

public class AdvertsMyHandler : IRequestHandler<AdvertsMyRequest, IEnumerable<AdvertsMyResponse>>
{
    private readonly IAdvertRepository _advertRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AdvertsMyHandler(IAdvertRepository advertRepository, IHttpContextAccessor httpContextAccessor)
    {
        _advertRepository = advertRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<IEnumerable<AdvertsMyResponse>> Handle(AdvertsMyRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId))
        {
            throw new ApplicationException("Unauthorized");
        }

        IEnumerable<AdvertsMyResponse> adverts = _advertRepository.GetBySellerId(userId)
            .Select(advert => new AdvertsMyResponse(
                Id: advert.Id.ToString(),
                SellerId: advert.SellerId.ToString(),
                Title: advert.Title,
                Description: advert.Description,
                Location: advert.Location,
                Price: advert.Price,
                Images: advert.Images,
                CategoryId: advert.CategoryId
            )).ToList();

        return Task.FromResult(adverts);
    }
}
