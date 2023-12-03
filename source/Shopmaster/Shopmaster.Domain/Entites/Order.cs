namespace Shopmaster.Domain.Entites;

public enum OrderStatus : int
{
    Waiting = 0,
    Canceled = 1,
    Confirmed = 2,
    Delivery = 3,
    Complete = 4
}

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId {get;set;}
    public User Customer {get;set;}
    public Guid AdvertId { get; set; }
    public Advert Advert { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Waiting;
    public DateTime DateCreated { get; set; }
}