using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Infrastructure.Persistence;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public CategoryRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void Add(Category category)
    {
        _applicationDbContext.Categories.Add(category);
        _applicationDbContext.SaveChanges();
    }

    public IEnumerable<Category> GetAll()
    {
        return _applicationDbContext.Categories;
    }

    public Category? GetById(int categoryId)
    {
        return _applicationDbContext.Categories
            .FirstOrDefault(category => category.Id == categoryId);
    }

    public Category? GetByName(string categoryName)
    {
        return _applicationDbContext.Categories
            .FirstOrDefault(category => category.Name == categoryName);
    }

    public void Remove(Category category)
    {
        _applicationDbContext.Categories.Remove(category);
        _applicationDbContext.SaveChanges();
    }

    public void Update(Category category)
    {
        _applicationDbContext.SaveChanges();
    }
}
