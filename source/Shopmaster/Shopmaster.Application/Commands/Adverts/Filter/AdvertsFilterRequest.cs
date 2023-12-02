using MediatR;
using Shopmaster.Application.Common.Dtos;

namespace Shopmaster.Application.Commands.Filter;

public record AdvertsFilterRequest(
    int Offset,
    int Count,
    string? Title,
    Guid? SellerId,
    int? CategoryId,
    RangeFilterDto? Price,
    RangeFilterDto? Rating
) : IRequest<IEnumerable<AdvertsFilterResponse>>;