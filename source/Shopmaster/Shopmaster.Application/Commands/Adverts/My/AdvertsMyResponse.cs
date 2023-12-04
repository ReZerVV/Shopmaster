using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Adverts.My;

public record AdvertsMyResponse(
    string Id,
    string SellerId,
    string Title,
    string Description,
    Location Location,
    Price Price,
    IEnumerable<string> Images,
    int CategoryId
);