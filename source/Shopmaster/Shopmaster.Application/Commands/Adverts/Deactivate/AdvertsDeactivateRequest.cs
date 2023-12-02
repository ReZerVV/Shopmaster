using MediatR;

namespace Shopmaster.Application.Commands.Adverts.Deactivate;

public record AdvertsDeactivateRequest(
    Guid Id
) : IRequest<AdvertsDeactivateResponse>;