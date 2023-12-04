using MediatR;

namespace Shopmaster.Application.Commands.Categories.GetAll;

public record CategoriesGetAllRequest(
) : IRequest<IEnumerable<CategoriesGetAllResponse>>;