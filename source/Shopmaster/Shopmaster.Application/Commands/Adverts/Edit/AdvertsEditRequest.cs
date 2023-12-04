using MediatR;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Adverts.Edit;

public record AdvertsEditRequest(
    Guid Id,
    string Title,
    string Description,
    Location Location,
    Price Price,
    int CategoryId
) : IRequest<AdvertsEditResponse>;