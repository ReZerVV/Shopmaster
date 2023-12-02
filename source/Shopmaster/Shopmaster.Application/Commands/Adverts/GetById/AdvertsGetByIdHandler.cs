using System.Net.Mime;
using MediatR;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.GetById;

public class AdvertsGetByIdHandler : IRequestHandler<AdvertsGetByIdRequest, AdvertsGetByIdResponse>
{
    private readonly IAdvertRepository _advertRepository;

    public AdvertsGetByIdHandler(IAdvertRepository advertRepository)
    {
        _advertRepository = advertRepository;
    }

    public Task<AdvertsGetByIdResponse> Handle(AdvertsGetByIdRequest request, CancellationToken cancellationToken)
    {
        if (_advertRepository.GetById(request.Id) is not Advert advert)
        {
            throw new ApplicationException("Advert with given id not found");
        }

        return Task.FromResult(
            new AdvertsGetByIdResponse(
                Id: advert.Id.ToString(),
                SellerId: advert.SellerId.ToString(),
                Title: advert.Title,
                Description: advert.Description,
                Location: advert.Location,
                Price: advert.Price,
                Images: advert.Images,
                CategoryId: advert.CategoryId
            )
        );
    }
}
