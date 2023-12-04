using MediatR;

namespace Shopmaster.Application.Commands.Adverts.Activate;

public record AdvertsActivateRequest(
    Guid Id
) : IRequest<AdvertsActivateResponse>;