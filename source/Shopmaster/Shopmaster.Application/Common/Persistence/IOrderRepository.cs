using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Common.Persistence;

public interface IOrderRepository
{
    void Add(Order order);
    void Update(Order order);
    void Remove(Order order);
    Order? GetById(Guid orderId);
    IEnumerable<Order> GetByCustomerId(Guid customerId);
    IEnumerable<Order> GetByAdvertId(Guid advertId);
}