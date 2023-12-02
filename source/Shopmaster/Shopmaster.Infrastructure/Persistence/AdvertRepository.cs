using Microsoft.EntityFrameworkCore;
using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Infrastructure.Persistence;

public class AdvertRepository : IAdvertRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AdvertRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void Add(Advert advert)
    {
        _applicationDbContext.Adverts.Add(advert);
        _applicationDbContext.SaveChanges();
    }

    public void Remove(Advert advert)
    {
        _applicationDbContext.Adverts.Remove(advert);
        _applicationDbContext.SaveChanges();
    }

    public IEnumerable<Advert> GetAll(int offset, int count)
    {
        return _applicationDbContext.Adverts
            .Include(advert => advert.Seller)
            .Include(advert => advert.Category)
            .Skip(offset)
            .Take(count);
    }

    public IEnumerable<Advert> GetByFilter(int offset, int count, string? title = null, int? categoryId = null, Guid? sellerId = null, int? maxPrice = null, int? minPrice = null, int? maxRating = null, int? minRating = null)
    {
        return _applicationDbContext.Adverts
            .Include(advert => advert.Seller)
            .Include(advert => advert.Category)
            .Where(advert => 
                advert.IsActive &&
                (title == null || advert.Title.Contains(title)) &&
                (categoryId == null || advert.CategoryId == categoryId) && 
                (sellerId == null || advert.SellerId == sellerId) &&
                ((minPrice == null || maxPrice == null) || (advert.Price.Value >= minPrice && advert.Price.Value < maxPrice)))
            .Skip(offset)
            .Take(count);
    }

    public Advert? GetById(Guid advertId)
    {
        return _applicationDbContext.Adverts
            .Include(advert => advert.Seller)
            .Include(advert => advert.Category)
            .FirstOrDefault(advert => advert.IsActive && advert.Id == advertId);
    }

    public void Update(Advert advert)
    {
        _applicationDbContext.SaveChanges();
    }
}
