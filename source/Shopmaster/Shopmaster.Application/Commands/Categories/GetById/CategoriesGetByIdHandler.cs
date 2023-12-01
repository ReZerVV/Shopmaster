using MediatR;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Categories.GetById;

public class CategoriesGetByIdHandler : IRequestHandler<CategoriesGetByIdRequest, CategoriesGetByIdResponse>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesGetByIdHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task<CategoriesGetByIdResponse> Handle(CategoriesGetByIdRequest request, CancellationToken cancellationToken)
    {
        if (_categoryRepository.GetById(request.Id) is not Category category)
        {
            throw new ApplicationException("Category with given id not found");
        }

        return Task.FromResult(
            new CategoriesGetByIdResponse(
                Id: category.Id,
                Name: category.Name
            )
        );
    }
}
