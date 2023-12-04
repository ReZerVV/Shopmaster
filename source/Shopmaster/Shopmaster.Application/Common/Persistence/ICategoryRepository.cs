using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Common.Persistence;

public interface ICategoryRepository
{
    void Add(Category category);
    void Update(Category category);
    void Remove(Category category);
    IEnumerable<Category> GetAll();
    Category? GetById(int categoryId);
    Category? GetByName(string categoryName);
}