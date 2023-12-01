namespace Shopmaster.Domain.Entites;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> Products { get; set; }
}