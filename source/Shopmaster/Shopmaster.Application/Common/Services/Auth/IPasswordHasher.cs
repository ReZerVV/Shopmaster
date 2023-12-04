namespace Shopmaster.Application.Common.Services.Auth;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string paswordHash);
}