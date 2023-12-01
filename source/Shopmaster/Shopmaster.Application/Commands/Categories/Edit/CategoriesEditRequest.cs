using MediatR;

namespace Shopmaster.Application.Commands.Categories.Edit;

public record CategoriesEditRequest(
    int Id,
    string Name
) : IRequest<CategoriesEditResponse>;