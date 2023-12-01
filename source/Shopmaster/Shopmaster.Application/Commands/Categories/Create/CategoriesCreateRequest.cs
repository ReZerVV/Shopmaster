using MediatR;

namespace Shopmaster.Application.Commands.Categories.Create;

public record CategoriesCreateRequest(
    string Name
) : IRequest<CategoriesCreateResponse>;