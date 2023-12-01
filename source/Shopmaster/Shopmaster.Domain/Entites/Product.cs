namespace Shopmaster.Domain.Entites;

public class Product
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public User Seller { get; set; }
}