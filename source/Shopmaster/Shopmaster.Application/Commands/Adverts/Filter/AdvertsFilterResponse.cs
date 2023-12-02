using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Filter;

public record AdvertsFilterResponse(
    string Id,
    string SellerId,
    string Title,
    string Description,
    Location Location,
    Price Price,
    IEnumerable<string> Images,
    int CategoryId
);