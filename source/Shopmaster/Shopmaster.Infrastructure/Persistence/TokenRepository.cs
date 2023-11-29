using Shopmaster.Application.Common.Persistence;
using Shopmaster.Domain.Entites;

namespace Shopmaster.Infrastructure.Persistence;

public class TokenRepository : ITokenRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public TokenRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public void Add(RefreshToken refreshToken)
    {
        _applicationDbContext.RefreshTokens.Add(refreshToken);
        _applicationDbContext.SaveChanges();
    }

    public RefreshToken? GetByUserId(Guid id)
    {
        return _applicationDbContext.RefreshTokens
            .FirstOrDefault(refreshToken => refreshToken.UserId == id);
    }

    public void Remove(RefreshToken refreshToken)
    {
        _applicationDbContext.RefreshTokens.Remove(refreshToken);
        _applicationDbContext.SaveChanges();
    }

    public void Update(RefreshToken refreshToken)
    {
        _applicationDbContext.SaveChanges();
    }
}
