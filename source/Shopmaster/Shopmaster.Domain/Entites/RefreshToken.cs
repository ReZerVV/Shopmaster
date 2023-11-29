namespace Shopmaster.Domain.Entites;

public class RefreshToken
{
    public Guid Id {get;set;}
    public Guid UserId {get;set;}
    public string? Token {get;set;}
    public DateTime Expiry {get;set;}
}