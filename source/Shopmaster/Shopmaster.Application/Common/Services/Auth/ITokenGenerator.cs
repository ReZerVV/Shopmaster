using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Common.Services.Auth;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}