namespace Shopmaster.Domain.Entites;

public class Advert
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public User Seller { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Location Location { get; set; }
    public Price Price { get; set; }
    public IEnumerable<string> Images { get; set; } = new List<string>();
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public bool IsActive { get; set; } = false;
}