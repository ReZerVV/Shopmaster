using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.GetById;

public record AdvertsGetByIdResponse(
    string Id,
    string SellerId,
    string Title,
    string Description,
    Location Location,
    Price Price,
    IEnumerable<string> Images,
    int CategoryId
);