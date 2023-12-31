using System.ComponentModel.DataAnnotations;
using MediatR;
using Shopmaster.Application.Common.Persistence;

namespace Shopmaster.Application.Commands.Adverts.Filter;

public class AdvertsFilterHandler : IRequestHandler<AdvertsFilterRequest, IEnumerable<AdvertsFilterResponse>>
{
    private readonly IAdvertRepository _advertRepository;

    public AdvertsFilterHandler(IAdvertRepository advertRepository)
    {
        _advertRepository = advertRepository;
    }

    public Task<IEnumerable<AdvertsFilterResponse>> Handle(AdvertsFilterRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<AdvertsFilterResponse> adverts = _advertRepository.GetByFilter(
            request.Offset,
            request.Count,
            title: request.Title,
            categoryId: request.CategoryId,
            sellerId: request.SellerId,
            maxPrice: request.maxPrice,
            minPrice: request.minPrice,
            maxRating: request.maxRating,
            minRating: request.minRating)
            .Select(advert => new AdvertsFilterResponse(
                Id: advert.Id.ToString(),
                SellerId: advert.SellerId.ToString(),
                Title: advert.Title,
                Description: advert.Description,
                Location: advert.Location,
                Price: advert.Price,
                Images: advert.Images,
                CategoryId: advert.CategoryId
            ));

        return Task.FromResult(adverts);
    }
}
