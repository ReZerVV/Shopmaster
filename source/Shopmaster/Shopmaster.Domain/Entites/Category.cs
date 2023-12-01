namespace Shopmaster.Domain.Entites;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Advert> Adverts { get; set; } = new List<Advert>();
}