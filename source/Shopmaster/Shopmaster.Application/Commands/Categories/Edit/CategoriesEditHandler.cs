using MediatR;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Categories.Edit;

public class CategoriesEditHandler : IRequestHandler<CategoriesEditRequest, CategoriesEditResponse>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesEditHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task<CategoriesEditResponse> Handle(CategoriesEditRequest request, CancellationToken cancellationToken)
    {
        if (_categoryRepository.GetById(request.Id) is not Category category)
        {
            throw new ApplicationException("Category with given id not found");
        }

        category.Name = request.Name;

        _categoryRepository.Update(category);

        return Task.FromResult(
            new CategoriesEditResponse(
                Id: category.Id,
                Name: category.Name
            )
        );
    }
}
