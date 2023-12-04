namespace Shopmaster.Domain.Entites;

public class User
{
    public Guid Id { get; set; }
    public string? Image { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool IsConfirmed { get; set; } = false;
    public bool IsRecovery { get; set; } = false;
    public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    public IEnumerable<Advert> Adverts { get; set; } = new List<Advert>();
}