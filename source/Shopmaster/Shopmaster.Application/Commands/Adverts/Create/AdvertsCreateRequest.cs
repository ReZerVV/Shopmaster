using MediatR;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Adverts.Create;

public record AdvertsCreateRequest(
    string Title,
    string Description,
    Location Location,
    Price Price,
    IEnumerable<string> Images,
    int CategoryId
) : IRequest<AdvertsCreateResponse>;