using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Common.Persistence;

public interface ITokenRepository
{
    void Add(RefreshToken refreshToken);
    RefreshToken? GetByUserId(Guid id);
    void Remove(RefreshToken refreshToken);
    void Update(RefreshToken refreshToken);
}