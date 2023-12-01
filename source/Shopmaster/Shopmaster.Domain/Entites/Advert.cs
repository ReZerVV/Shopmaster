namespace Shopmaster.Domain.Entites;

public class Advert
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public User Seller { get; set; }
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();
}