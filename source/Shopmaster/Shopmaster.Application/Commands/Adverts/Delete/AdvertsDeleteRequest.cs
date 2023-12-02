using MediatR;

namespace Shopmaster.Application.Commands.Adverts.Delete;

public record AdvertsDeleteRequest(
    Guid Id
) : IRequest<AdvertsDeleteResponse>;