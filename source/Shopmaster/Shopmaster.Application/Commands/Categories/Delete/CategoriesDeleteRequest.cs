using MediatR;

namespace Shopmaster.Application.Commands.Categories.Delete;

public record CategoriesDeleteRequest(
    int Id
) : IRequest;