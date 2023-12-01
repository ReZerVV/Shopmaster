namespace Shopmaster.Domain.Entites;

public class Price
{
    public string Currency { get; set; }
    public decimal Value { get; set; }
}

public class Location
{
    public string City { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
}

public class Advert
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public User Seller { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Location Location {get;set;}
    public Price Price { get; set; }
    public IEnumerable<string> Images { get; set; } = new List<string>();
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();
}