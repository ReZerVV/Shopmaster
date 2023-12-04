using MediatR;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Commands.Categories.Delete;

public class CategoriesDeleteHandler : IRequestHandler<CategoriesDeleteRequest>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesDeleteHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task Handle(CategoriesDeleteRequest request, CancellationToken cancellationToken)
    {
        if (_categoryRepository.GetById(request.Id) is not Category category)
        {
            throw new ApplicationException("Category with given id not found");
        }

        _categoryRepository.Remove(category);

        return Task.CompletedTask;
    }
}
