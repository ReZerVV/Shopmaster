using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Common.Persistence;

public interface IAdvertRepository
{
    void Add(Advert advert);
    void Remove(Advert advert);
    void Update(Advert advert);
    Advert? GetById(Guid advertId);
    IEnumerable<Advert> GetAll(int offset, int count);
    IEnumerable<Advert> GetByFilter(int offset, int count, string? title = null, int? categoryId = null, Guid? sellerId = null, int? maxPrice = null, int? minPrice = null, int? maxRating = null, int? minRating = null);
}