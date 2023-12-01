using MediatR;

namespace Shopmaster.Application.Commands.Categories.GetById;

public record CategoriesGetByIdRequest(
    int Id
) : IRequest<CategoriesGetByIdResponse>;