using MediatR;
using Shopmaster.Application.Common.Dtos;

namespace Shopmaster.Application.Commands.Adverts.Filter;

public record AdvertsFilterRequest(
    int Offset,
    int Count,
    string? Title,
    Guid? SellerId,
    int? CategoryId,
    int? minPrice,
    int? maxPrice,
    int? minRating,
    int? maxRating
) : IRequest<IEnumerable<AdvertsFilterResponse>>;