using MediatR;

namespace Shopmaster.Application.Commands.GetById;

public record AdvertsGetByIdRequest(
    Guid Id
) : IRequest<AdvertsGetByIdResponse>;