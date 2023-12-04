using MediatR;

namespace Shopmaster.Application.Commands.Adverts.My;

public record AdvertsMyRequest(
) : IRequest<IEnumerable<AdvertsMyResponse>>;