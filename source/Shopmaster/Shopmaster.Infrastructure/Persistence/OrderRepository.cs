using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Infrastructure.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public OrderRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void Add(Order order)
    {
        _applicationDbContext.Orders.Add(order);
        _applicationDbContext.SaveChanges();
    }

    public IEnumerable<Order> GetByAdvertId(Guid advertId)
    {
        return _applicationDbContext.Orders
            .Where(order => order.AdvertId == advertId);
    }

    public IEnumerable<Order> GetByCustomerId(Guid customerId)
    {
        return _applicationDbContext.Orders
            .Where(order => order.CustomerId == customerId);
    }

    public Order? GetById(Guid orderId)
    {
        return _applicationDbContext.Orders
            .FirstOrDefault(order => order.Id == orderId);
    }

    public void Remove(Order order)
    {
        _applicationDbContext.Orders.Remove(order);
        _applicationDbContext.SaveChanges();
    }

    public void Update(Order order)
    {
        _applicationDbContext.SaveChanges();
    }
}
