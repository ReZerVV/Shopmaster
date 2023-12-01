using MediatR;
using Shopmaster.Application.Common.Persistence;

namespace Shopmaster.Application.Commands.Categories.GetAll;

public class CategoriesGetAllHandler : IRequestHandler<CategoriesGetAllRequest, IEnumerable<CategoriesGetAllResponse>>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesGetAllHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task<IEnumerable<CategoriesGetAllResponse>> Handle(CategoriesGetAllRequest request, CancellationToken cancellationToken)
    {
        var categories = _categoryRepository.GetAll()
            .Select(category => new CategoriesGetAllResponse(
                Id: category.Id,
                Name: category.Name
            ));

        return Task.FromResult(categories);
    }
}
