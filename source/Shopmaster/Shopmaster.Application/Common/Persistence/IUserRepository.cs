using Shopmaster.Domain.Entites;

namespace Shopmaster.Application.Common.Persistence;

public interface IUserRepository
{
    void Add(User user);
    void Update(User user);
    void Remove(User user);
    User? GetByEmail(string email);
    User? GetById(Guid id);
}