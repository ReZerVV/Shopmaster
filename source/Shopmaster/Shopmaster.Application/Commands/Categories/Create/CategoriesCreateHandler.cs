using MediatR;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Categories.Create;

public class CategoriesCreateHandler : IRequestHandler<CategoriesCreateRequest, CategoriesCreateResponse>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesCreateHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task<CategoriesCreateResponse> Handle(CategoriesCreateRequest request, CancellationToken cancellationToken)
    {
        if (_categoryRepository.GetByName(request.Name) is not null)
        {
            throw new ApplicationException("Category with given name already exists");
        }

        Category category = new Category
        {
            Name = request.Name,
        };

        _categoryRepository.Add(category);

        return Task.FromResult(
            new CategoriesCreateResponse(
                Id: category.Id,
                Name: category.Name
            )
        );
    }
}
